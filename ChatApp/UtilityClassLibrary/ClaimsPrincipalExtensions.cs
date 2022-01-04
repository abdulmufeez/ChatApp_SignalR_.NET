using System.Security.Claims;

namespace ChatApp.UtilityClassLibrary
{
    public static class ClaimsPrincipalExtensions
    {
        // this will added a custom function User class
        //which will be access like this User.GetUserId()
        public static string GetUserId(this ClaimsPrincipal @this)
        {
            return @this.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}