﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjectEntity.Entity
{
    public class Position : BaseModel
    {
        [Required(ErrorMessage ="Name is required!")]
        public string Name { get; set; }
        public List<Team>? Teams { get; set; }


    }
}
