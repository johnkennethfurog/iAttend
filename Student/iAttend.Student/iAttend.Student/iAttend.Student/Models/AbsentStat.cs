using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class AbsentStat
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("absents")]
        public List<Absent> Absents { get; set; }
    }
}
