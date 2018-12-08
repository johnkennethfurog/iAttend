using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class PayloadLogin
    {
        [JsonProperty("instructorNumber")]
        public string InstructorNumber { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
