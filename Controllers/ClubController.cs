using clubs_api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace clubs_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubController : ControllerBase
    {
        [HttpGet]
        [Route("getClubs")]
        public IActionResult GetClubs()
        {   
            var repository = new ClubSqlRepository();
            var clubs = repository.GetClubs();
            return Ok(clubs);
        }
    }
}