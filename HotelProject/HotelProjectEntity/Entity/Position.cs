﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjectEntity.Entity
{
    public class Position : BaseModel
    {
        public string Name { get; set; }
        public List<Team> Teams { get; set; }


    }
}
