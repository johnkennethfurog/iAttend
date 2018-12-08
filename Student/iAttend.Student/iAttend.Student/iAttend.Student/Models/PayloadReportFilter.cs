using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    class PayloadReportFilter
    {
        [JsonProperty("subjects")]
        public List<TeacherSubject> Subjects { get; set; }

        [JsonProperty("dateFrom")]
        public DateTime DateFrom { get; set; }

        [JsonProperty("dateTo")]
        public DateTime DateTo { get; set; }
    }
}
