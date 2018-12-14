using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    class PayloadUnMarkStudentAttendance
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("studentNumber")]
        public string StudentNumber { get; set; }

        [JsonProperty("scheduleId")]
        public int ScheduleId { get; set; }

    }
    class PayloadMarkStudentAttendance : PayloadUnMarkStudentAttendance
    {

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("studentName")]
        public string StudentName { get; set; }
    }
}
