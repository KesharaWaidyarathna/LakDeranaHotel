using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakDeranaHotel.Model
{
    class MarksDAO
    {
        public int id { get; set; }

        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public int Marks { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public string Discription { get; set; }

    }
}
