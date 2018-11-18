using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    class PayloadTeacherFetchStudent
    {
        [JsonProperty("scheduleId")]
        public int ScheduleId { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }
    }
}
