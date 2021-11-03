using clubs_api.Application.Services;
using clubs_api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace clubs_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioClubController : ControllerBase
    {
        [HttpGet]
        [Route("getServicios")]
        public IActionResult GetServicios()
        {
            var repository = new ServicioClubSqlRepository();
            var srv = new ServicioToDtoService();
            var servicios = repository.GetServicios();
            var response = srv.ObjectsToDtos(servicios);

            return Ok(response);
        }

        [HttpGet]
        [Route("getServicio/{id::int}")]
        public IActionResult GetServicioById(int id)
        {
            var repository = new ServicioClubSqlRepository();
            var srv = new ServicioToDtoService();
            var servicio = repository.GetServicioById(id);
            var response = srv.ObjectToDto(servicio);
            
            return Ok(response);
        }

    }
}