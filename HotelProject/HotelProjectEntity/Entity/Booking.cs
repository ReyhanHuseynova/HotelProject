using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjectEntity.Entity
{
    public class Booking:BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public string AdultCount { get; set; }
        public string ChildCount { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
    }
}
