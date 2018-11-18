using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class StudentSubject
    {
        [JsonProperty("scheduleID")]
        public int ScheduleID { get; set; }

        [JsonProperty("studentNumber")]
        public string StudentNumber { get; set; }

        [JsonProperty("instructorNumber")]
        public string InstructorNumber { get; set; }

        [JsonProperty("instructor")]
        public string Instructor { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("room")]
        public string Room { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("dayOfWeek")]
        public string DayOfWeek { get; set; }

        [JsonProperty("subjectCode")]
        public string SubjectCode { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }
    }
}
