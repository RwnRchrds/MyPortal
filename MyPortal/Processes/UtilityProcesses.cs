using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using MyPortal.Models;
using MyPortal.Models.Misc;
using Syncfusion.EJ2.Base;

namespace MyPortal.Processes
{
    public static class UtilityProcesses
    {
        public static ApiResponse<T> PerformDataOperations<T>(this IEnumerable<T> dataSource, DataManagerRequest dm)
        {
            DataOperations operation = new DataOperations();

            if (dm.Search != null && dm.Search.Count > 0)
            {
                dataSource = operation.PerformSearching(dataSource, dm.Search);
            }

            if (dm.Sorted != null && dm.Sorted.Count > 0)
            {
                dataSource = operation.PerformSorting(dataSource, dm.Sorted);
            }

            if (dm.Where != null && dm.Where.Count > 0)
            {
                dataSource = operation.PerformFiltering(dataSource, dm.Where, dm.Where[0].Operator);
            }

            var count = dataSource.Count();

            if (dm.Skip != 0)
            {
                dataSource = operation.PerformSkip(dataSource, dm.Skip);
            }

            if (dm.Take != 0)
            {
                dataSource = operation.PerformTake(dataSource, dm.Take);
            }

            return new ApiResponse<T> { Count = count, Items = dataSource };
        }

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

        public static string GenerateId()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}