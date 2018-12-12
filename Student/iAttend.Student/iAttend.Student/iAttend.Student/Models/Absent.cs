using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class Absent
    {

        [JsonProperty("studentNumber")]
        public string StudentNumber { get; set; }

        [JsonProperty("studentName")]
        public string StudentName { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("subjectCode")]
        public string SubjectCode { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("present")]
        public long PresentCount { get; set; }

        [JsonProperty("absent")]
        public long AbsentCount { get; set; }

        [JsonProperty("totalAttendance")]
        public long TotalAttendanceCount { get; set; }

        [JsonProperty("instructorNumber")]
        public string InstructorNumber { get; set; }
    }
}
