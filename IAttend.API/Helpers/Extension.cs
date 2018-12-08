using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace IAttend.API.Helpers
{
    public static class Extension
    {
        public static string ToDayInWord(this int day)
        {
            switch(day)
            {
                case 1:
                return "Monday";
                case 2:
                return "Tuesday";
                case 3:
                return "Wendesday";
                case 4:
                return "Thursday";
                case 5:
                return "Friday";
                case 6:
                return "Saturday";
                default:
                return "Sunday";
            }
        }


        public static string GetInstructorNumber(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value;
        }
    }
}