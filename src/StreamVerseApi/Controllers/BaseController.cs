using Microsoft.AspNetCore.Mvc;
using StreamVerseApi.Data;

namespace StreamVerseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        public readonly DataContext Context;

        public BaseController(DataContext dataContext) 
        {
            Context  = dataContext;
        }
    }
}
