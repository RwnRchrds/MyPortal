using MyPortal.Database.Models.Identity;
using MyPortal.Logic.Helpers;
using MyPortal.Logic.Models.Details;

namespace MyPortal.Logic.Extensions
{
    public static class UserExtensions
    {
        public static string GetDisplayName(this ApplicationUser user, bool salutationFormat)
        {
            var mapper = MappingHelper.GetBusinessConfig();

            if (user.Person != null)
            {
                var personDetails = mapper.Map<PersonDetails>(user.Person);

                return personDetails.GetDisplayName(salutationFormat);
            }
            else
            {
                return user.UserName;
            }
        }
    }
}