using System;
using System.Threading.Tasks;
using MyPortal.Logic.Models.Requests.Finance;

namespace MyPortal.Logic.Interfaces.Services;

public interface IAccountTransactionService
{
    Task CreateAccountTransaction(params CreateAccountTransactionRequestModel[] models);
    Task DeleteAccountTransaction(params Guid[] transactionIds);
}