using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjectEntity.Entity
{
    public class Service:BaseModel
    {
        public string ServiceTitle { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
