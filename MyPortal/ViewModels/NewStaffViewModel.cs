using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls.WebParts;
using MyPortal.Controllers.Api;
using MyPortal.Models;

namespace MyPortal.ViewModels
{
    public class NewStaffViewModel
    {
        public NewStaffViewModel()
        {
            Titles = new List<string>() {"Mr","Miss","Mrs","Ms","Mx","Prof","Sir","Dr","Lady","Lord"};       
        }
        public Staff Staff { get; set; }
        public IEnumerable<string> Titles { get; set; }        
    }
}