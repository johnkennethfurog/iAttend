using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Helpers
{
    public class Endpoint
    {
        public const string STUDENT_CONFIRM = "api/student/confirm/{0}";
        public const string STUDENT_SUBJECT = "api/student/subjects/{0}";
        public const string STUDENT_ATTENDANCE = "api/student/{0}/attendances/{1}";
        public const string STUDENT_MARK_ATTENDANCE = "api/student/attendance";

        public const string TEACHER_SUBJECT = "api/teacher/subjects";
        public const string TEACHER_STUDENT = "api/teacher/{0}/attendance/{1}";
        public const string TEACHER_MARK_STUDENT_ATTENDANCE = "api/teacher/attendance";
        public const string TEACHER_UNMARK_STUDENT_ATTENDANCE = "api/teacher/attendance";
        public const string TEACHER_ATTENDANCE_START = "api/teacher/{0}/{1}/start";
        public const string TEACHER_ATTENDANCES_STOP = "api/teacher/{0}/{1}/stop";
        public const string TEACHER_ATTENDANCES_STOP_ALL = "api/teacher/{0}/{1}/stopAll";
        public const string TEACHER_REPORT = "api/teacher/generateReport";
        public const string TEACHER_LOGIN = "api/auth/login";
        public const string TEACHER_STUDENT_ABSENT = "api/teacher/studentAbsent";
    }
}
