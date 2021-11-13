using clubs_api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using clubs_api.Application.Services;
using clubs_api.Domain.Interfaces;
using System.Threading.Tasks;

namespace clubs_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TorneoController : ControllerBase
    {
        private readonly ITorneoSqlRepository repository;
        public TorneoController(ITorneoSqlRepository _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        [HttpGet("getTorneos")]
        public async Task<IActionResult> GetTorneos()
        {
            var repository = new TorneoSqlRepository();
            var srv = new TorneoToDtoService();
            var torneos = await repository.GetTorneos();
            var response = srv.ObjectsToDtos(torneos);
            return Ok(response);
        }

        [HttpGet]
        [Route("getTorneo/{id::int}")]
        public async Task<IActionResult> GetTorneoById(int id){
            var repository = new TorneoSqlRepository();
            var srv = new TorneoToDtoService();
            var torneo = await repository.GetTorneoById(id);
            if (torneo == null)
                return NotFound("No se ha encontrado un torneo que corresponda con el ID proporcionado");
            var response = srv.ObjectToDto(torneo);

            return Ok(response);
        }
    }
}