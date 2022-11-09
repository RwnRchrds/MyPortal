using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Interfaces.Services;
using MyPortal.Logic.Models.Entity;

namespace MyPortal.Logic.Services
{
    public class BillService : BaseService, IBillService
    {
        public async Task<IEnumerable<BillModel>> GenerateChargeBills(Guid chargeBillingPeriodId)
        {
            await using (var unitOfWork = await DataConnectionFactory.CreateUnitOfWork())
            {
                ChargeBillingPeriod chargeBillingPeriod = await unitOfWork.ChargeBillingPeriods.GetById(chargeBillingPeriodId);

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
                    
                    var chargeDiscounts =
                        (await unitOfWork.ChargeDiscounts.GetByStudent(billableStudent.Key)).ToArray();

                    foreach (var studentCharge in billableStudent)
                    {
                        bill.BillStudentCharges.Add(new BillStudentCharge
                        {
                            StudentChargeId = studentCharge.Id,
                            NetAmount = studentCharge.Charge.Amount,
                            VatAmount = studentCharge.Charge.Amount * studentCharge.Charge.VatRate.Value
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
