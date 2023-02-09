using System;
using MyPortal.Logic.Attributes;
using MyPortal.Logic.Enums;

namespace MyPortal.Logic.Models.Requests.Finance;

public class AccountTransactionRequestModel
{
    [NotDefault]
    public Guid StudentId { get; set; }
    
    [Currency]
    public decimal Amount { get; set; }

    public bool Credit { get; set; }
    
    [DateTimeMode(DateTimeMode.PastOrPresent)]
    public DateTime Date { get; set; }
}