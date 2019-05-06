using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace IAttend.API.Helpers
{
    public static class Extension
    {

        public static void AddPagination(this HttpResponse response,
            int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage,itemsPerPage,totalItems,totalPages);
            
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("pagination",JsonConvert.SerializeObject(paginationHeader,camelCaseFormatter));
            response.Headers.Add("Access-Control-Expose-Headers","pagination");
        }

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

        public static string GetEmail(this ClaimsPrincipal user)
        {
            return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
        }
    }
}