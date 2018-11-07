using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class Subject
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("dayOfWeek")]
        public string DayOfWeek { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
