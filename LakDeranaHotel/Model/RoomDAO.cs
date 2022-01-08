using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LakDeranaHotel.Model
{
    class RoomDAO
    {
        public int Roomid { get; set; }
        public string RoomCategory { get; set; }
        public int BedCount { get; set; }
        public int Price { get; set; }
        public string Note { get; set; }
        public bool IsReserved { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime Todate { get; set; }
    }
}
