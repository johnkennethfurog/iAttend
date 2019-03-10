using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class PayloadAddSchedule
    {
        [JsonProperty("subjectCode")]
        public string SubjectCode { get; set; }

        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("timeFrom")]
        public DateTime TimeFrom { get; set; }

        [JsonProperty("timeTo")]
        public DateTime TimeTo { get; set; }

        [JsonProperty("dayOfWeek")]
        public int DayOfWeek { get; set; }
    }
}
