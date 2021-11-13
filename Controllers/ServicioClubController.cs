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
        public ServicioClubController(IServicioClubSqlRepository _repository)
        {
              repository = _repository; 
        }

        [HttpGet]
        [Route("getServicios")]
        public async Task<IActionResult> GetServicios()
        {
            var repository = new ServicioClubSqlRepository();
            var srv = new ServicioToDtoService();
            var servicios = await repository.GetServicios();
            var response = srv.ObjectsToDtos(servicios);

            return Ok(response);
        }

        [HttpGet]
        [Route("getServicio/{id::int}")]
        public async Task<IActionResult> GetServicioById(int id)
        {
            var repository = new ServicioClubSqlRepository();
            var srv = new ServicioToDtoService();
            var servicio = await repository.GetServicioById(id);
            if (servicio == null)
                return NotFound("No se ha encontrado un servicio que corresponda con el ID proporcionado");
            var response = srv.ObjectToDto(servicio);
            
            return Ok(response);
        }

    }
}