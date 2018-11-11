using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Helpers
{
    public class Endpoint
    {
        public const string STUDENT_SUBJECT = "student/subjects/{0}";
        public const string STUDENT_ATTENDANCE = "student/{0}/attendances/{1}";
        public const string STUDENT_MARK_ATTENDANCE = "student/attendance";
    }
}
