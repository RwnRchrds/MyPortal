using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using MyPortal.Models;
using MyPortal.Models.Misc;

namespace MyPortal.Processes
{
    public class DiagnosticProcesses
    {
        public static async Task<ProcessResponse<object>> CreateSystemUser(UserManager<ApplicationUser, string> userManager)
        {
            var userInDb = await userManager.FindByNameAsync("system");

            if (userInDb != null)
            {
                await userManager.DeleteAsync(userInDb);
            }

            var newId = Guid.NewGuid().ToString();

            var user = new ApplicationUser
            {
                Id = newId,
                UserName = "system"
            };

            await userManager.CreateAsync(user, "education");
            await userManager.AddToRoleAsync(newId, "Admin");

            return new ProcessResponse<object>(ResponseType.Ok, null, null);
        }
    }
}