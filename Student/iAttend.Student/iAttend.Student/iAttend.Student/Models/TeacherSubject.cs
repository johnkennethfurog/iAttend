using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class TeacherSubject
    {
        [JsonProperty("schedID")]
        public int SchedID { get; set; }

        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("dayOfWeek")]
        public string DayOfWeek { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("studentCount")]
        public int StudentCount { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("isOpen")]
        public bool IsOpen { get; set; }

        [JsonIgnore]
        public string Tag => $"{Code} ({Time})";
    }
}
