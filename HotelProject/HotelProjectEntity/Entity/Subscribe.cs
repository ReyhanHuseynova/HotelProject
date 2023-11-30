using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjectEntity.Entity
{
    public class Subscribe:BaseModel
    {
        [Required(ErrorMessage ="Email is required!")]
        [EmailAddress(ErrorMessage ="Invalid email adress!")]
        public string Email { get; set; }
    }
}
