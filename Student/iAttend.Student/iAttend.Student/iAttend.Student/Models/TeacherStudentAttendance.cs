using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class TeacherStudentAttendance
    {
        [JsonProperty("studentNumber")]
        public string StudentNumber { get; set; }

        [JsonProperty("studentName")]
        public string StudentName { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("isPresent")]
        public bool IsPresent { get; set; }
    }
}
