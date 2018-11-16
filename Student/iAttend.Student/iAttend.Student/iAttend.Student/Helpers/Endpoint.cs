using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Helpers
{
    public class Endpoint
    {
        public const string STUDENT_SUBJECT = "api/student/subjects/{0}";
        public const string STUDENT_ATTENDANCE = "api/student/{0}/attendances/{1}";
        public const string STUDENT_MARK_ATTENDANCE = "api/student/attendance";

        public const string TEACHER_SUBJECT = "api/teacher/{0}/subjects";
        public const string TEACHER_STUDENT = "{0}/attendance/{1}";
        public const string TEACHER_MARK_STUDENT_ATTENDANCE = "api/teacher/attendance";
        public const string TEACHER_UNMARK_STUDENT_ATTENDANCE = "api/teacher/attendance";
    }
}
