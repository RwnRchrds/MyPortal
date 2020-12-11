using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyPortal.Database.Constants;
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
        private readonly IAccountTransactionRepository _accountTransactionRepository;
        private readonly IBillRepository _billRepository;
        private readonly IBillAccountTransactionRepository _billAccountTransactionRepository;
        private readonly IBillChargeRepository _billChargeRepository;
        private readonly IBillDiscountRepository _billDiscountRepository;
        private readonly IBillItemRepository _billItemRepository;
        private readonly IChargeRepository _chargeRepository;
        private readonly IChargeDiscountRepository _chargeDiscountRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IStudentChargeRepository _studentChargeRepository;
        private readonly IStudentDiscountRepository _studentDiscountRepository;
        private readonly ISystemSettingRepository _settingRepository;

        public BillService(IAccountTransactionRepository accountTransactionRepository, IBillRepository billRepository,
            IBillAccountTransactionRepository billAccountTransactionRepository,
            IBillChargeRepository billChargeRepository, IBillDiscountRepository billDiscountRepository,
            IBillItemRepository billItemRepository, IChargeRepository chargeRepository,
            IChargeDiscountRepository chargeDiscountRepository, IDiscountRepository discountRepository,
            IStudentChargeRepository studentChargeRepository, IStudentDiscountRepository studentDiscountRepository,
            ISystemSettingRepository settingRepository)
        {
            _accountTransactionRepository = accountTransactionRepository;
            _billRepository = billRepository;
            _billAccountTransactionRepository = billAccountTransactionRepository;
            _billChargeRepository = billChargeRepository;
            _billDiscountRepository = billDiscountRepository;
            _billItemRepository = billItemRepository;
            _chargeRepository = chargeRepository;
            _chargeDiscountRepository = chargeDiscountRepository;
            _discountRepository = discountRepository;
            _studentChargeRepository = studentChargeRepository;
            _studentDiscountRepository = studentDiscountRepository;
            _settingRepository = settingRepository;
        }

        public async Task<IEnumerable<BillModel>> GenerateChargeBills()
        {
            var hasSetting = int.TryParse((await _settingRepository.Get(SystemSettings.BillPaymentPeriodLength)).Setting, out int paymentPeriodLength);

            if (!hasSetting)
            {
                throw new LogicException("Bill payment period length not defined.");
            }

            var billableStudents = (await _studentChargeRepository.GetOutstanding()).GroupBy(sc => sc.StudentId);

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

                    var chargeInDb = await _studentChargeRepository.GetByIdWithTracking(charge.ChargeId);
                    chargeInDb.Recurrences--;
                }

                var studentDiscounts = await _studentDiscountRepository.GetByStudent(billableStudent.Key);

                foreach (var studentDiscount in studentDiscounts)
                {
                    var applicableChargeIds =
                        (await _chargeDiscountRepository.GetByDiscount(studentDiscount.DiscountId)).Select(x =>
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

                _billRepository.Create(bill);
                generatedBills.Add(bill);
            }

            await _billRepository.SaveChanges();

            return generatedBills.Select(BusinessMapper.Map<BillModel>);
        }


        public override void Dispose()
        {
            _accountTransactionRepository?.Dispose();
            _billRepository?.Dispose();
            _billAccountTransactionRepository?.Dispose();
            _billChargeRepository?.Dispose();
            _billDiscountRepository?.Dispose();
            _billItemRepository?.Dispose();
            _chargeRepository?.Dispose();
            _chargeDiscountRepository?.Dispose();
            _discountRepository?.Dispose();
            _studentChargeRepository?.Dispose();
            _studentDiscountRepository?.Dispose();
            _settingRepository?.Dispose();
        }
    }
}
