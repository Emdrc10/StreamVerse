using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StreamVerse.Domain.Core;

namespace StreamVerse.Domain.Entities
{
    public class Rating : BaseEntity
    {

        public int score { get; set; }
        public string review { get; set; }
        public int userId { get; set; }
        public  User? user { get; set; }
        public int? movieId { get; set; }
        public Movie? movie { get; set; }
        public int? serieId { get; set; }
        public Serie? serie { get; set; }
        public string? created { get; set; }
        public string? updated { get; set; }

    }
}
