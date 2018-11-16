using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    class PayloadMarkStudentAttendance
    {
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("studentNumber")]
        public string StudentNumber { get; set; }

        [JsonProperty("scheduleId")]
        public int ScheduleId { get; set; }
    }
}
