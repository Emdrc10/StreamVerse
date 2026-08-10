using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamVerse.Aplication.Models.Dtos
{
    public class CreateRatingDto
    {
        public int Score { get; set; }
        public string? Review { get; set; }
        public int UserId { get; set; }
        public int? MovieId { get; set; }
        public int? SerieId { get; set; }
    }
}
