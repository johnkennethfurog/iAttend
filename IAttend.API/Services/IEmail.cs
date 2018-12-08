using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Services
{
    public interface IEmail
    {
        Task<bool> SendEmail(string excelFilePath,string sendTo,string header,string subject);
    }
}
