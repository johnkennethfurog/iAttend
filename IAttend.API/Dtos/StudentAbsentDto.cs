using IAttend.API.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IAttend.API.Dtos
{
    public class StudentAbsentDto
    {
        public int Count { get; set; }
        public List<StudentsAbsentStat> Absents { get; set; }
    }
}
