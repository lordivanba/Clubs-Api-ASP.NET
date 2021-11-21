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
    public class ClubController : ControllerBase
    {
        private readonly IClubSqlRepository repository;
        private readonly IClubService service;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ClubController(IClubSqlRepository _repository, IClubService _service, IHttpContextAccessor _httpContextAccessor)
        {
            repository = _repository;
            service = _service;
            httpContextAccessor = _httpContextAccessor;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetClubs()
        {   
            var clubs = await repository.GetClubs();
            var response = service.ObjectsToDtos(clubs);
            return Ok(response);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetClubById(int id)
        {
            var club = await repository.GetClubById(id);
            if (club == null)
                return NotFound("No se ha encontrado un club que corresponda con el ID proporcionado");
            var response = service.ObjectToDto(club);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetByFilter")]
        public async Task<IActionResult> GetClubByFilter(ClubFilterDto dto)
        {
            var club = service.DtoToObject(dto);
            var clubs = await repository.GetByFilter(club);

            if (!clubs.Any())
                return NotFound("No se ha encontrado un club que coincida con la informacion proporcionada");

            var response = service.ObjectsToDtos(clubs);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClub(Club club)
        {
            var validate = service.ValidateCreate(club);
            if (!validate)
                return UnprocessableEntity("El registro no puede ser realizado a falta de informacion");
            int id;
            try
            {
                id = await repository.CreateClub(club);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            if (id <= 0)
                return Conflict("El registro puede ser realizado, verifica tu informacion");
            var host = httpContextAccessor.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/Club/{id}";

            return Created(urlResult, id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateClub(int id, [FromBody] Club club)
        {
            if (id <= 0)
                return NotFound("No se encontro un registro que coincida con la informacion proporcionada");

            if (club == null)
                return UnprocessableEntity("La actualizacion no puede ser realizada a falta de informacion");
            club.Id = id;

            var validate = service.ValidateUpdate(club);
            if (!validate)
                return UnprocessableEntity("La actualizacion no puede ser realizada, verifica tu informacion");
            try
            {
                var result = await repository.UpdateClub(id,club);
                if (!result)
                    return Conflict("No fue posible realizar la actualizacion, verifica su informacion");
            }catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "No es posible realizar la actualizacion");
            }

            return NoContent();
        }
    }
}