using Microsoft.AspNetCore.Mvc;
using StreamVerseApi.Models.Entities;

namespace StreamVerseApi.Controllers
{

    [ApiController]
    [Route("api/[Controller]")]
    public class GenresController : ControllerBase
    {
        private static readonly List<Genre> _genres = new List<Genre>
        {
            new Genre
            {
                Id = 1,
                Name = "Action",
                Description = "High energy films with physical stunts and combat."
            },
            new Genre
            {
                Id = 2,
                Name = "Comedy",
                Description = "Films designed to entertain and make the audience laugh."
            },
            new Genre
            {
                Id = 3,
                Name = "Horror",
                Description = "Films designed to frighten and unsettle the audience."
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Genre>> GetAll()
        {
            return Ok(_genres);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Genre> GetById(int id)
        {
            var genre = _genres.FirstOrDefault(g => g.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            return Ok(genre);
        }

        [HttpPost]
        public ActionResult<Genre> Create(Genre genre)
        {
            genre.Id = _genres.Max(g => g.Id) + 1;
            _genres.Add(genre);
            return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);
        }   

        [HttpPut]
        [Route("{id}")]
        public ActionResult Update(int id, Genre updatedGenre)
        {
            var genre = _genres.FirstOrDefault(g => g.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            genre.Name = updatedGenre.Name;
            genre.Description = updatedGenre.Description;
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            var genre = _genres.FirstOrDefault(g => g.Id == id);
            if (genre == null)
            {
                return NotFound();
            }
            _genres.Remove(genre);
            return NoContent();
        }
    }
}
