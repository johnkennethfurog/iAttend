using IAttend.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Data
{
    public interface IAuthentication
    {
        Task<Instructor> Login(string instructorNumber, string password);
        Task<bool> ResetPassword(string instructorNumber);
        Task<bool> SetPassword(string instructorNumber,string password);
    }
}
