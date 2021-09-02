using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Models.Entity;
using MyPortal.Logic.Models.Data;

namespace MyPortal.Logic.Models.Entity
{
    public class CommentBankModel : LookupItemModel
    {
        public CommentBankModel(CommentBank model) : base(model)
        {
            
        }
    }
}
