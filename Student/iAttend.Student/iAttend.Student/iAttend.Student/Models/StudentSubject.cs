using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class StudentSubject
    {
        [JsonProperty("instructor")]
        public Instructor Instructor { get; set; }

        [JsonProperty("subject")]
        public Subject Subject { get; set; }
    }
}
