using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakDeranaHotel.Model
{
    class CustomerDAO
    {
        public int idNo { get; set; }
        public string firstName { get; set; }
        public string lastname { get; set; }

        public DateTime DOB { get; set; }

        public string gender { get; set; }

        public string address { get; set; }

        public string NIC { get; set; }

        public int phone { get; set; }

        public byte[] image { get; set; }

    }
}
