using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace IAttend.API.Services
{
    public class Communication : ICommunication
    {
        private readonly IHttpClientFactory _clientFactory;

        public Communication(IHttpClientFactory clientFactory )
        {
            _clientFactory = clientFactory;
        }

        public string GenerateSmsMessageForGuardian(string studentName, string subject, string time)
        {
            return $"This message is to inform you that {studentName} had entered his class {subject} for {time} at { DateTime.Now.ToShortTimeString()} today";
        }

        public async Task<bool> SendEmail(string excelFilePath, string sendTo, string header, string subject)
        {
            // Create a message and set up the recipients.
            MailMessage message = new MailMessage(
               "johnkennethfurog@gmail.com",
               sendTo,
               header,
               subject);

            // Create  the file attachment for this email message.
            Attachment data = new Attachment(excelFilePath, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(excelFilePath);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(excelFilePath);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(excelFilePath);
            // Add the file attachment to this email message.
            message.Attachments.Add(data);

            //Send the message.
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("johnkennethfurog@gmail.com", "NowYouSeeMe!!")

            };

            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                            ex.ToString());

                return false;
            }

            data.Dispose();

            return await Task.FromResult(true);
        }

        public async Task SendSms(string message, string number)
        {
            var httpClient = _clientFactory.CreateClient();

            var uri = $"http://192.168.1.52/iattendapi/send.php?mobile={number}&message={message}";

            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            var response = await httpClient.SendAsync(request);

            //var result = await response.Content.ReadAsStringAsync();

        }
    }
}
