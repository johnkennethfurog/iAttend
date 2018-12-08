using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class StudentInfo
    {
        [JsonProperty("studentNumber")]
        public string StudentNumber { get; set; }

        [JsonProperty("studentName")]
        public string StudentName { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }


        [JsonIgnore]
        public string StudentNameCapital => StudentName?.ToUpper();
    }
}
