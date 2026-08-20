using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamVerse.Domain.Core;

namespace StreamVerse.Domain.Entities
{
    public class Favorite : BaseEntity
    {
        public int UserId { get; set; }
        public User? User { get; set; }
        public int? MovieId { get; set; }
        public Movie? Movie { get; set; }
        public int? SerieId { get; set; }
        public Serie? Serie { get; set; }
        public string? Created { get; set; }
    }
}
