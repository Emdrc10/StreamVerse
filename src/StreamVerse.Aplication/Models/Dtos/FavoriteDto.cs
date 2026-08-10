using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamVerse.Aplication.Models.Dtos
{
    public class FavoriteDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? MovieTitle { get; set; }
        public string? SerieTitle { get; set; }
        public string? Created { get; set; }
    }
}
