using clubs_api.Application.Services;
using clubs_api.Domain.Interfaces;
using clubs_api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace clubs_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioClubController : ControllerBase
    {
        private readonly IServicioClubSqlRepository repository;
        private readonly IServicioClubService service;
        public ServicioClubController(IServicioClubSqlRepository _repository, IServicioClubService _service)
        {
            repository = _repository;
            service = _service;
                
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetServicios()
        {
            var servicios = await repository.GetServicios();
            var response = service.ObjectsToDtos(servicios);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetServicioById(int id)
        {
            var servicio = await repository.GetServicioById(id);
            if (servicio == null)
                return NotFound("No se ha encontrado un servicio que corresponda con el ID proporcionado");
            var response = service.ObjectToDto(servicio);
            
            return Ok(response);
        }

    }
}