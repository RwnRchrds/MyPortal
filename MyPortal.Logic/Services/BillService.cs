using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database;
using MyPortal.Database.Constants;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories;
using MyPortal.Logic.Exceptions;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Logic.Services
{
    public class BillService : BaseService, IBillService
    {
        public async Task<IEnumerable<BillModel>> GenerateChargeBills()
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                var hasSetting = int.TryParse((await unitOfWork.SystemSettings.Get(SystemSettings.BillPaymentPeriodLength)).Setting, out int paymentPeriodLength);

                if (!hasSetting)
                {
                    throw new LogicException("Bill payment period length not defined.");
                }

                var billableStudents = (await unitOfWork.StudentCharges.GetOutstanding()).GroupBy(sc => sc.StudentId);

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

                        var chargeInDb = await unitOfWork.StudentCharges.GetByIdForEditing(charge.ChargeId);
                        chargeInDb.Recurrences--;
                    }

                    var studentDiscounts = await unitOfWork.StudentDiscounts.GetByStudent(billableStudent.Key);

                    foreach (var studentDiscount in studentDiscounts)
                    {
                        var applicableChargeIds =
                            (await unitOfWork.ChargeDiscounts.GetByDiscount(studentDiscount.DiscountId)).Select(x =>
                                x.ChargeId);

                        if (bill.BillCharges.Any(c => applicableChargeIds.Contains(c.ChargeId)))
                        {
                            bill.BillDiscounts.Add(new BillDiscount
                            {
                                DiscountId = studentDiscount.DiscountId,
                                GrossAmount = studentDiscount.Discount.Amount,
                            });
                        }
                    }

                    unitOfWork.Bills.Create(bill);
                    generatedBills.Add(bill);
                }

                await unitOfWork.SaveChangesAsync();

                return generatedBills.Select(BusinessMapper.Map<BillModel>);
            }
        }
    }
}
