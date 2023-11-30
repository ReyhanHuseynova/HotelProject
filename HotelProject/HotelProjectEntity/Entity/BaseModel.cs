using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjectEntity.Entity
{
    public class BaseModel
    {
        public int Id { get; set; }
        public bool IsDeactive { get; set; }
        public DateTime  CreateDate { get; set; }
        public DateTime  UpdateDate { get; set; }
    }
}


