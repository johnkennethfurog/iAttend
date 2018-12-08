using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace iAttend.Student.Models
{
    public class Token
    {
        [JsonProperty("tokenString")]
        public string TokenString { get; set; }
    }
}
