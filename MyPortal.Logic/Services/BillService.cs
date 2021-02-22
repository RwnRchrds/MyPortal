using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class BillService : BaseService, IBillService
    {
        public BillService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<IEnumerable<BillModel>> GenerateChargeBills()
        {
            var hasSetting = int.TryParse((await UnitOfWork.SystemSettings.Get(SystemSettings.BillPaymentPeriodLength)).Setting, out int paymentPeriodLength);

            if (!hasSetting)
            {
                throw new LogicException("Bill payment period length not defined.");
            }

            var billableStudents = (await UnitOfWork.StudentCharges.GetOutstanding()).GroupBy(sc => sc.StudentId);

            var generatedBills = new List<Bill>();

            foreach (var billableStudent in billableStudents)
            {
                var bill = new Bill
                {
                    CreatedDate = DateTime.Now,
                    StudentId = billableStudent.Key,
                    DueDate = DateTime.Today.AddMonths(paymentPeriodLength)
                };

                foreach (var charge in billableStudent)
                {
                    bill.BillCharges.Add(new BillCharge
                    {
                        ChargeId = charge.ChargeId,
                        GrossAmount = charge.Charge.Amount
                    });

                    var chargeInDb = await UnitOfWork.StudentCharges.GetByIdForEditing(charge.ChargeId);
                    chargeInDb.Recurrences--;
                }

                var studentDiscounts = await UnitOfWork.StudentDiscounts.GetByStudent(billableStudent.Key);

                foreach (var studentDiscount in studentDiscounts)
                {
                    var applicableChargeIds =
                        (await UnitOfWork.ChargeDiscounts.GetByDiscount(studentDiscount.DiscountId)).Select(x =>
                            x.ChargeId);

                    if (bill.BillCharges.Any(c => applicableChargeIds.Contains(c.ChargeId)))
                    {
                        bill.BillDiscounts.Add(new BillDiscount
                        {
                            DiscountId = studentDiscount.DiscountId,
                            Amount = studentDiscount.Discount.Amount,
                            Percentage = studentDiscount.Discount.Percentage
                        });
                    }
                }

                UnitOfWork.Bills.Create(bill);
                generatedBills.Add(bill);
            }

            await UnitOfWork.SaveChanges();

            return generatedBills.Select(BusinessMapper.Map<BillModel>);
        }
    }
}
