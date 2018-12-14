using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class PayloadStudentAttendance
    {
        [JsonProperty("attendanceSessionId")]
        public int AttendanceSessionId { get; set; }

        [JsonProperty("studentNumber")]
        public string StudentNumber { get; set; }

        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("studentName")]
        public string StudentName { get; set; }
    }

    public class SchedulePayload
    {
        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

    }
}
