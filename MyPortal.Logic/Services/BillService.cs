using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IEnumerable<BillModel>> GenerateChargeBills(Guid chargeBillingPeriodId)
        {
            using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                // TODO: Get from database
                ChargeBillingPeriod chargeBillingPeriod = new ChargeBillingPeriod();

                var billableStudents =
                    (await unitOfWork.StudentCharges.GetOutstandingByBillingPeriod(chargeBillingPeriodId)).GroupBy(sc =>
                        sc.StudentId);

                var generatedBills = new List<Bill>();

                foreach (var billableStudent in billableStudents)
                {
                    var bill = new Bill
                    {
                        CreatedDate = DateTime.Now,
                        StudentId = billableStudent.Key,
                        DueDate = chargeBillingPeriod.EndDate
                    };

                    // TODO: Get by student
                    var chargeDiscounts = new List<ChargeDiscount>();

                    foreach (var studentCharge in billableStudent)
                    {
                        bill.BillStudentCharges.Add(new BillStudentCharge
                        {
                            StudentChargeId = studentCharge.Id,
                            GrossAmount = studentCharge.Charge.Amount
                        });

                        
                        // Check for discounts and apply
                        var applicableDiscounts = chargeDiscounts
                            .Where(cd => cd.ChargeId == studentCharge.ChargeId).ToArray();

                        foreach (var chargeDiscount in applicableDiscounts)
                        {
                            bill.BillChargeDiscounts.Add(new BillDiscount
                            {
                                DiscountId = chargeDiscount.DiscountId,
                                GrossAmount = chargeDiscount.Discount.Percentage ? studentCharge.Charge.Amount * (100 / chargeDiscount.Discount.Amount) : chargeDiscount.Discount.Amount
                            });
                        }
                    }

                    unitOfWork.Bills.Create(bill);
                    generatedBills.Add(bill);
                }

                // TODO: Do we want to immediately write these to the database?
                // Might be better to let the user review the bills first before saving
                await unitOfWork.SaveChangesAsync();

                return generatedBills.Select(b => new BillModel(b));
            }
        }
    }
}
