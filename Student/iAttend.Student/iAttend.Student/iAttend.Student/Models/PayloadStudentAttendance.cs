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
    }
}
