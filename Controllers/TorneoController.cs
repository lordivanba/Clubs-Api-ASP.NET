using clubs_api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using clubs_api.Application.Services;

namespace clubs_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TorneoController : ControllerBase
    {
        [HttpGet]
        [HttpGet("getTorneos")]
        public IActionResult GetTorneos()
        {
            var repository = new TorneoSqlRepository();
            var srv = new TorneoToDtoService();
            var torneos = repository.GetTorneos();
            var response = srv.ObjectsToDtos(torneos);

            return Ok(response);
        }

        [HttpGet]
        [Route("getTorneo/{id::int}")]
        public IActionResult GetTorneoById(int id){
            var repository = new TorneoSqlRepository();
            var srv = new TorneoToDtoService();
            var torneo = repository.GetTorneoById(id);
            var response = srv.ObjectToDto(torneo);

            return Ok(response);
        }
    }
}