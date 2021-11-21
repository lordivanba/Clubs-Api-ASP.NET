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
        private readonly ITorneoService service;
        public TorneoController(ITorneoSqlRepository _repository, ITorneoService _service)
        {
            repository = _repository;
            service = _service;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetTorneos()
        {
            var torneos = await repository.GetTorneos();
            var response = service.ObjectsToDtos(torneos);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetTorneoById(int id){
            var torneo = await repository.GetTorneoById(id);
            if (torneo == null)
                return NotFound("No se ha encontrado un torneo que corresponda con el ID proporcionado");
            var response = service.ObjectToDto(torneo);

            return Ok(response);
        }
    }
}