using clubs_api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using clubs_api.Application.Services;
using clubs_api.Domain.Interfaces;
using System.Threading.Tasks;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using AutoMapper;
using clubs_api.Domain.Dtos.Responses;
using System.Collections.Generic;
using clubs_api.Domain.Dtos.Requests;
using FluentValidation;

namespace clubs_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TorneoController : ControllerBase
    {
        private readonly ITorneoSqlRepository repository;
        private readonly ITorneoService service;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IValidator<TorneoCreateRequest> createValidator;
        private readonly IValidator<TorneoUpdateRequest> updateValidator;

        public TorneoController(
            ITorneoSqlRepository _repository,
            ITorneoService _service,
            IHttpContextAccessor _httpContextAccessor,
            IMapper _mapper,
            IValidator<TorneoCreateRequest> _createValidator,
            IValidator<TorneoUpdateRequest> _updateValidator)
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
        public async Task<IActionResult> GetTorneos()
        {
            var torneos = await repository.GetTorneos();
            var response = mapper.Map<IEnumerable<Torneo>, IEnumerable<TorneoResponseDto>>(torneos);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetTorneoById(int id)
        {
            var torneo = await repository.GetTorneoById(id);
            if (torneo == null)
                return NotFound("No se ha encontrado un torneo que corresponda con el ID proporcionado");
            var response = mapper.Map<Torneo, TorneoResponseDto>(torneo);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id::int}/participantes")]
        public async Task<IActionResult> GetTorneoWithParticipantesById(int id)
        {
            var torneo = await repository.GetTorneoById(id);
            if (torneo == null)
                return NotFound("No se ha encontrado un torneo que corresponda con el ID proporcionado");
            var response = mapper.Map<Torneo, TorneoResponseDto>(torneo);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetByFilter")]
        public async Task<IActionResult> GetTorneoByFilter(TorneoFilterDto dto)
        {
            var torneo = mapper.Map<TorneoFilterDto, Torneo>(dto);
            var torneos = await repository.GetByFilter(torneo);

            if (!torneos.Any())
                return NotFound("No se ha encontrado un torneo que coincida con la informacion proporcionada");

            var response = mapper.Map<IEnumerable<Torneo>, IEnumerable<TorneoResponseDto>>(torneos);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTorneo(TorneoCreateRequest torneo)
        {
            var validate = await createValidator.ValidateAsync(torneo);
            if(!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(x => $"{x.PropertyName} => {x.ErrorMessage}"));

            var obj = mapper.Map<TorneoCreateRequest, Torneo>(torneo);
            int id;

            try
            {
                id = await repository.CreateTorneo(obj);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            if (id <= 0)
                return Conflict("El registro no puede ser realizado, verifica tu informacion");

            var host = httpContextAccessor.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/Torneo/{id}";

            return Created(urlResult, id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateTorneo(int id, [FromBody] TorneoUpdateRequest torneo)
        {
            if (id <= 0)
                return NotFound("No se encontro un registro que coincida con la informacion proporcionada");
            if (torneo == null)
                return UnprocessableEntity("La actualizacion no puede ser realizada a falta de informacion");

            var validate = await updateValidator.ValidateAsync(torneo);
            if(!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(x => $"{x.PropertyName} => {x.ErrorMessage}"));

            var obj = mapper.Map<TorneoUpdateRequest, Torneo>(torneo);
            torneo.Id = id;

            try
            {
                var result = await repository.UpdateTorneo(id, obj);
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