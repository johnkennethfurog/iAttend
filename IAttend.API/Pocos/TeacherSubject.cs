﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Pocos
{
    public class TeacherSubject
    {
        public int ID { get; set; }

        public string Room { get; set; }

        public DateTime TimeFrom { get; set; }

        public DateTime TimeTo { get; set; }

        public int DayOfWeek { get; set; }

        public string SubjectCode { get; set; }

        public string Subject { get; set; }

        public int StudCount { get; set; }

        public int IsOpen { get; set; }
    }
}
