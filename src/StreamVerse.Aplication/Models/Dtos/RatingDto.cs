using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamVerse.Aplication.Models.Dtos
{
    public class RatingDto
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string? Review { get; set; }
        public string? UserName { get; set; }
        public string? MovieTitle { get; set; }
        public string? SerieTitle { get; set; }
    }
}
