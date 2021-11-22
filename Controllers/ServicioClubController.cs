using clubs_api.Application.Services;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Entities;
using clubs_api.Domain.Interfaces;
using clubs_api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace clubs_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicioClubController : ControllerBase
    {
        private readonly IServicioClubSqlRepository repository;
        private readonly IServicioClubService service;
        private readonly IHttpContextAccessor httpContextAccessor;
        public ServicioClubController(IServicioClubSqlRepository _repository, IServicioClubService _service, IHttpContextAccessor _httpContextAccessor)
        {
            repository = _repository;
            service = _service;
            httpContextAccessor = _httpContextAccessor;
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

        [HttpGet]
        [Route("GetByFilter")]
        public async Task<IActionResult> GetServicioByFilter(ServicioClubFilterDto dto)
        {
            var servicio = service.DtoToObject(dto);
            var servicios = await repository.GetByFilter(servicio);

            if (!servicios.Any())
                return NotFound("No se ha encontrado un servicio que coincida con la informacion proporcionada");
            var response = service.ObjectsToDtos(servicios);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateServicio(ServicioClub servicio)
        {
            var validate = service.ValidateCreate(servicio);
            if (!validate)
                return UnprocessableEntity("El registro no puede ser realizado a falta de informacion");
            int id;
            try
            {
                id = await repository.CreateServicio(servicio);
            }
            catch (Exception e){
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
            if (id <= 0)
                return Conflict("El registro puede ser realizado, verifica tu informacion");
            var host = httpContextAccessor.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/ServicioClub/{id}";

            return Created(urlResult, id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateServicio(int id, [FromBody] ServicioClub servicio)
        {
            if(id <= 0)
                return NotFound("No se encontro un registro que coincida con la informacion proporcionada");
            if(servicio == null)
                return UnprocessableEntity("La actualizacion no puede ser realizada a falta de informacion");
            servicio.Id = id;

            var validate = service.ValidateUpdate(servicio);
            if (!validate)
                return UnprocessableEntity("La actualizacion no puede ser realizada, verifica tu informacion");
            try
            {
                var result = await repository.UpdateServicio(id, servicio);
                if (!result)
                    return Conflict("No fue posible realizar la actualizacion, verifica su informacion");
            }
            catch (Exception e) {
                return StatusCode(StatusCodes.Status500InternalServerError, "No es posible realizar la actualizacion");
            }
            return NoContent();
        }

    }
}