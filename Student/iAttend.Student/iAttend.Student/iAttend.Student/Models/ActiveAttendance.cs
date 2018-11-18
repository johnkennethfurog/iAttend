using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class ActiveAttendance
    {
        [JsonProperty("schedId")]
        public int SchedId { get; set; }

        [JsonProperty("attendanceSessionId")]
        public int AttendanceSessionId { get; set; }
    }
}
