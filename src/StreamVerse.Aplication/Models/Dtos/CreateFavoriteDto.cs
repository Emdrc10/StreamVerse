using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamVerse.Aplication.Models.Dtos
{
    public class CreateFavoriteDto
    {
        public int UserId { get; set; }
        public int? MovieId { get; set; }
        public int? SerieId { get; set; }
    }
}
