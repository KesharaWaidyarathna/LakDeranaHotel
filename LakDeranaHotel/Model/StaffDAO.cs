using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakDeranaHotel.Model
{
    class StaffDAO
    {
        public int EmployeeId { get; set; }

        public string EmployeeType { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DOB { get; set; }

        public string gender { get; set; }

        public string address { get; set; }

        public string NIC { get; set; }

        public int phone { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public byte[] image { get; set; }


    }
}
