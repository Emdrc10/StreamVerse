using Microsoft.AspNetCore.Mvc;
using StreamVerseApi.Models.Entities;

namespace StreamVerseApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class SeriesController : ControllerBase
    {
        private static readonly List<Serie> _serie = new List<Serie>
        {
            new Serie
            {
                Id = 1,
                Title = "Breaking Bad",
                Year = 2008,
                Seasons = 5,
                Episodes = 62,
                Synopsis = "A chemistry teacher turned methamphetamine manufacturer.",
                Poster = "breaking-bad.jpg",
                Created = DateTime.UtcNow.ToString(),
                Updated = DateTime.UtcNow.ToString()
            },
            new Serie
            {
                Id = 2,
                Title = "Stranger Things",
                Year = 2016,
                Seasons = 4,
                Episodes = 34,
                Synopsis = "A group of kids uncover supernatural mysteries in their town.",
                Poster = "stranger-things.jpg",
                Created = DateTime.UtcNow.ToString(),
                Updated = DateTime.UtcNow.ToString()
            },
            new Serie
            {
                Id = 3,
                Title = "The Last of Us",
                Year = 2023,
                Seasons = 2,
                Episodes = 16,
                Synopsis = "A smuggler and a teenage girl traverse a post-apocalyptic world.",
                Poster = "the-last-of-us.jpg",
                Created = DateTime.UtcNow.ToString(),
                Updated = DateTime.UtcNow.ToString()
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Serie>> GetAll()
        {
            return Ok(_serie);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Serie> GetById(int id)
        {
            var serie = _serie.FirstOrDefault(s => s.Id == id);
            if (serie == null)
            {
                return NotFound();
            }
            return Ok(serie);
        }

        [HttpPost]
        public ActionResult<Serie> Create(Serie serie)
        {
            serie.Id = _serie.Max(s => s.Id) + 1;
            serie.Created = DateTime.UtcNow.ToString();
            serie.Updated = DateTime.UtcNow.ToString();
            _serie.Add(serie);
            return CreatedAtAction(nameof(GetById), new { id = serie.Id }, serie);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Serie updatedSerie)
        {
            var serie = _serie.FirstOrDefault(s => s.Id == id);
            if (serie == null)
            {
                return NotFound();
            }
            serie.Title = updatedSerie.Title;
            serie.Year = updatedSerie.Year;
            serie.Seasons = updatedSerie.Seasons;
            serie.Episodes = updatedSerie.Episodes;
            serie.Synopsis = updatedSerie.Synopsis;
            serie.Poster = updatedSerie.Poster;
            serie.Updated = DateTime.UtcNow.ToString();
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var serie = _serie.FirstOrDefault(s => s.Id == id);
            if (serie == null)
            {
                return NotFound();
            }
            _serie.Remove(serie);
            return NoContent();
        }
    }
}
