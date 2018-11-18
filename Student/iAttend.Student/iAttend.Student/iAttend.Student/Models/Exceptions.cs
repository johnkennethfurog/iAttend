using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    class TeacherServiceException : BaseServiceException
    {
        public TeacherServiceException()
        {

        }
        public TeacherServiceException(string requestMethod, string errorMessage, string uriRequest, string payload = "", int statusCode = 0) : base(requestMethod, errorMessage, uriRequest, payload, statusCode)
        {
        }
    }

    class StudentServiceException : BaseServiceException
    {
        public StudentServiceException()
        {

        }
        public StudentServiceException(string requestMethod, string errorMessage, string uriRequest, string payload = "", int statusCode = 0) : base(requestMethod, errorMessage, uriRequest, payload, statusCode)
        {
        }
    }


    public abstract class BaseServiceException : Exception
    {
        public BaseServiceException()
        {

        }

        public BaseServiceException(string requestMethod, string errorMessage, string uriRequest, string payload = "", int statusCode = 0)
        {
            ExceptionMessage = errorMessage;
            Payload = payload;
            URI = uriRequest;
            RequestMethod = requestMethod;
            StatusCode = statusCode;
        }

        public string ExceptionMessage { get; set; }
        public int StatusCode { get; set; }

        public string Payload { get; set; }
        public string URI { get; set; }
        public string RequestMethod { get; set; }
    }
}
