using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjectEntity.Entity
{
    public class Room:BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public double Count { get; set; }
        public double Price { get; set; }
        public int BedCount { get; set; }
        public int BathCount { get; set; }
        public bool IsWifi { get; set; }
    }
}
