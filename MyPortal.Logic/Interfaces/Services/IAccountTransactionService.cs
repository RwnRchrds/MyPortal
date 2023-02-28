using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Finance;

namespace MyPortal.Logic.Interfaces.Services;

public interface IAccountTransactionService : IService
{
    Task CreateAccountTransaction(AccountTransactionRequestModel accountTransaction);
    Task DeleteAccountTransaction(Guid transactionId);
}