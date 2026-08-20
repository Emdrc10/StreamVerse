using Microsoft.AspNetCore.Mvc;
using StreamVerse.Infraestructure;


namespace StreamVerseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        public BaseController() { }
    }
}
