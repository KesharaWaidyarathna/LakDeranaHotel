using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakDeranaHotel.Model
{
    class AttendanceDAO
    {
        public int id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime LogedTime { get; set; }
        public DateTime LogedOut { get; set; }
        public DateTime Date { get; set; }
    }
}
