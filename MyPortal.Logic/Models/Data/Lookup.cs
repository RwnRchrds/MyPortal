using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.Mappers;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyPortal.Logic.Models.Data
{
    public class Lookup : Dictionary<string, Guid>
    {
        public Lookup()
        {

        }

        public Lookup(Dictionary<string, Guid> dictionary) : base(dictionary)
        {

        }

        public IEnumerable<SelectListItem> ToSelectList(string selectedValue = null)
        {
            var items = this.Select(x => new SelectListItem(x.Key, x.Value.ToString())).ToList();

            if (!string.IsNullOrWhiteSpace(selectedValue))
            {
                items.Add(new SelectListItem(selectedValue, null, true, true));
            }

            return items.OrderByDescending(x => x.Disabled).ThenBy(x => x.Text);
        }
    }
}
