using AutoMapper;
using clubs_api.Application.Services;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Dtos.Requests;
using clubs_api.Domain.Dtos.Responses;
using clubs_api.Domain.Entities;
using clubs_api.Domain.Interfaces;
using clubs_api.Infrastructure.Repositories;
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
    public class ServicioClubController : ControllerBase
    {
        private readonly IServicioClubSqlRepository repository;
        private readonly IServicioClubService service;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private readonly IValidator<ServicioClubCreateRequest> createValdiator;
        private readonly IValidator<ServicioClubUpdateRequest> updateValidator;
        public ServicioClubController(
            IServicioClubSqlRepository _repository,
            IServicioClubService _service,
            IHttpContextAccessor _httpContextAccessor,
            IMapper _mapper,
            IValidator<ServicioClubCreateRequest> _createValdiator,
            IValidator<ServicioClubUpdateRequest> _updateValdiator)
        {
            repository = _repository;
            service = _service;
            httpContextAccessor = _httpContextAccessor;
            mapper = _mapper;
            createValdiator = _createValdiator;
            updateValidator = _updateValdiator;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetServicios()
        {
            var servicios = await repository.GetServicios();
            var response = mapper.Map<IEnumerable<ServicioClub>, IEnumerable<ServicioClubResponseDto>>(servicios);

            return Ok(response);
        }

        [HttpGet]
        [Route("{id::int}")]
        public async Task<IActionResult> GetServicioById(int id)
        {
            var servicio = await repository.GetServicioById(id);
            if (servicio == null)
                return NotFound("No se ha encontrado un servicio que corresponda con el ID proporcionado");
            var response = mapper.Map<ServicioClub, ServicioClubResponseDto>(servicio);

            return Ok(response);
        }

        [HttpGet]
        [Route("GetByFilter")]
        public async Task<IActionResult> GetServicioByFilter(ServicioClubFilterDto dto)
        {
            var obj = mapper.Map<ServicioClubFilterDto, ServicioClub>(dto);
            var servicios = await repository.GetByFilter(obj);

            if (!servicios.Any())
                return NotFound("No se ha encontrado un servicio que coincida con la informacion proporcionada");

            var response = mapper.Map<IEnumerable<ServicioClub>, IEnumerable<ServicioClubResponseDto>>(servicios);
            
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateServicio(ServicioClubCreateRequest servicio)
        {
            var validate = await createValdiator.ValidateAsync(servicio);
            if (!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(x => $"{x.PropertyName} => {x.ErrorMessage}"));
            
            var obj = mapper.Map<ServicioClubCreateRequest, ServicioClub>(servicio);
            int id;

            try
            {
                id = await repository.CreateServicio(obj);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

            if (id <= 0)
                return Conflict("El registro no puede ser realizado, verifica tu informacion");
            
            var host = httpContextAccessor.HttpContext.Request.Host.Value;
            var urlResult = $"https://{host}/api/ServicioClub/{id}";

            return Created(urlResult, id);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateServicio(int id, [FromBody] ServicioClubUpdateRequest servicio)
        {
            if (id <= 0)
                return NotFound("No se encontro un registro que coincida con la informacion proporcionada");
            if (servicio == null)
                return UnprocessableEntity("La actualizacion no puede ser realizada a falta de informacion");
            servicio.Id = id;

            var validate = await updateValidator.ValidateAsync(servicio);    
            if (!validate.IsValid)
                return UnprocessableEntity(validate.Errors.Select(x => $"{x.PropertyName} => {x.ErrorMessage}"));

            var obj = mapper.Map<ServicioClubUpdateRequest, ServicioClub>(servicio);

            try
            {
                var result = await repository.UpdateServicio(id, obj);
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