using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjectEntity.Entity
{
    public class Service:BaseModel
    {
        [Required(ErrorMessage ="Service Title is required!")]
        public string ServiceTitle { get; set; }
        [Required(ErrorMessage ="Description is required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Icon is required!")]
        public string Icon { get; set; }
    }
}
