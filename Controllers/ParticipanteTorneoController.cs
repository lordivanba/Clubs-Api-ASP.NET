using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using clubs_api.Domain.Dtos.Requests;
using clubs_api.Domain.Dtos.Responses;
using clubs_api.Domain.Entities;
using clubs_api.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clubs_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParticipanteTorneoController : ControllerBase
    {
        private readonly IParticipanteTorneoSqlRepository repository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IValidator<ParticipanteTorneoCreateRequest> createValidator;

        public ParticipanteTorneoController(
            IParticipanteTorneoSqlRepository _repository,
            IHttpContextAccessor _httpContextAccessor,
            IMapper _mapper,
            IValidator<ParticipanteTorneoCreateRequest> _createValidator)
        {
            repository = _repository;
            httpContextAccessor = _httpContextAccessor;
            mapper = _mapper;
            createValidator = _createValidator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetParticipantes()
        {
            var participantes = await repository.GetParticipantes();
            var response = mapper.Map<IEnumerable<ParticipanteTorneo>, IEnumerable<ParticipanteTorneoResponseDto>>(participantes);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetParticipantesById(int id)
        {
            var participantes = await repository.GetParticipanteById(id);
            if(participantes == null)
                return NotFound("No se ha encontrado un participante que corresponda con el ID proporcionado");
            var response = mapper.Map<ParticipanteTorneo, ParticipanteTorneoResponseDto>(participantes);

            return Ok(response);
        }

        [HttpGet]
        [Route("Torneo/{id::int}")]
        public async Task<IActionResult> GetParticipantesByTorneoId(int id)
        {
            var participantes = await repository.GetParticipanteByTorneoId(id);
            if(!participantes.Any())
                return NotFound("No se ha encontrado ningun participante que coincida con la informacion proporcionada");
            var response = mapper.Map<IEnumerable<ParticipanteTorneo>, IEnumerable<ParticipanteTorneoResponseDto>>(participantes);

            return Ok(response);
        }

        [HttpGet]
        [Route("Club/{id::int}")]
        public async Task<IActionResult> GetParticipantesByClubId(int id)
        {
            var participantes = await repository.GetParticipanteByClubId(id);
             if(!participantes.Any())
                return NotFound("No se ha encontrado ningun participante que coincida con la informacion proporcionada");
            var response = mapper.Map<IEnumerable<ParticipanteTorneo>, IEnumerable<ParticipanteTorneoResponseDto>>(participantes);

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateParticipante(ParticipanteTorneoCreateRequest participante)
        {
            var validate = await createValidator.ValidateAsync(participante);
            if(!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(x => $"{x.PropertyName} => {x.ErrorMessage}"));

            var obj = mapper.Map<ParticipanteTorneoCreateRequest, ParticipanteTorneo>(participante);
            int id;

            
            try
            {
                id = await repository.CreateParticipante(obj);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            if (id <= 0)
                return Conflict("El registro no puede ser realizado, verifica tu informacion");

            var host = httpContextAccessor.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/ParticipanteTorneo/{id}";

            return Created(urlResult, id);
        }
    }
}