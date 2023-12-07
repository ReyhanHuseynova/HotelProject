using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelProjectEntity.Entity
{
    public class Team:BaseModel
    {
        public string FullName { get; set; }
        public string? Image { get; set; }
        public int SocialId { get; set; }
        public Social? Social { get; set; }
        public int PositionId { get; set; }
        public Position? Position { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}

