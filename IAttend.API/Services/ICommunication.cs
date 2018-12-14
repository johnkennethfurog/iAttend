using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Services
{
    public interface ICommunication
    {
        Task<bool> SendEmail(string excelFilePath,string sendTo,string header,string subject);
        Task SendSms(string message, string number);

        string GenerateSmsMessageForGuardian(string studentName, string subject, string time);
    }
}
