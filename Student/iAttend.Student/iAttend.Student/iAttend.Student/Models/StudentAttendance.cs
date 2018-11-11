using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class StudentAttendance
    {
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("isPresent")]
        public bool IsPresent { get; set; }
    }
}
