using AutoMapper;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Dtos.Requests;
using clubs_api.Domain.Dtos.Responses;
using clubs_api.Domain.Entities;
using clubs_api.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        private readonly IMapper mapper;
        private readonly IValidator<ClubCreateRequest> createValidator;
        private readonly IValidator<ClubUpdateRequest> updateValidator;

        public ClubController(
            IClubSqlRepository _repository,
            IClubService _service,
            IHttpContextAccessor _httpContextAccessor,
            IMapper _mapper,
            IValidator<ClubCreateRequest> _createValidator,
            IValidator<ClubUpdateRequest> _updateValidator)
        {
            repository = _repository;
            service = _service;
            httpContextAccessor = _httpContextAccessor;
            mapper = _mapper;
            createValidator = _createValidator;
            updateValidator = _updateValidator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetClubs()
        {
            var clubs = await repository.GetClubs();
            var response = mapper.Map<IEnumerable<Club>, IEnumerable<ClubResponseDto>>(clubs);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetClubById(int id)
        {
            var club = await repository.GetClubById(id);
            if (club == null)
                return NotFound("No se ha encontrado un club que corresponda con el ID proporcionado");
            var response = mapper.Map<Club, ClubResponseDto>(club);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetByFilter")]
        public async Task<IActionResult> GetClubByFilter(ClubFilterDto dto)
        {
            var club = mapper.Map<ClubFilterDto, Club>(dto);
            var clubs = await repository.GetByFilter(club);
            if (!clubs.Any())
                return NotFound("No se ha encontrado un club que coincida con la informacion proporcionada");
            var response = mapper.Map<IEnumerable<Club>, IEnumerable<ClubResponseDto>>(clubs);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateClub(ClubCreateRequest club)
        {
            var validate = await createValidator.ValidateAsync(club);
            if (!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(x => $"{x.PropertyName} => {x.ErrorMessage}"));

            var obj = mapper.Map<ClubCreateRequest, Club>(club);
            int id;

            try
            {
                id = await repository.CreateClub(obj);
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
        public async Task<IActionResult> UpdateClub(int id, [FromBody] ClubUpdateRequest club)
        {
            var obj = mapper.Map<ClubUpdateRequest, Club>(club);

            if (id <= 0)
                return NotFound("No se encontro un registro que coincida con la informacion proporcionada");

            if (obj == null)
                return UnprocessableEntity("La actualizacion no puede ser realizada a falta de informacion");
            obj.Id = id;

            var validate = await updateValidator.ValidateAsync(club);
            if (!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(x => $"{x.PropertyName} => {x.ErrorMessage}"));
                
            try
            {
                var result = await repository.UpdateClub(id, obj);
                if (!result)
                    return Conflict("No fue posible realizar la actualizacion, verifica su informacion");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "No es posible realizar la actualizacion");
            }

            return NoContent();
        }
    }
}