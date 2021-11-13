using clubs_api.Application.Services;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Interfaces;
using clubs_api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace clubs_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubController : ControllerBase
    {
        private readonly IClubSqlRepository repository;

        public ClubController(IClubSqlRepository _repository)
        {
            repository = _repository;
        }

        [HttpGet]
        [Route("getClubs")]
        public async Task<IActionResult> GetClubs()
        {   
            var repository = new ClubSqlRepository();
            var srv = new ClubToDtoService();
            var clubs = await repository.GetClubs();
            var response = srv.ObjectsToDtos(clubs);
            return Ok(response);
        }

        [HttpGet]
        [Route("getClub/{id::int}")]
        public async Task<IActionResult> GetClubById(int id)
        {
            var repository = new ClubSqlRepository();
            var srv = new ClubToDtoService();
            var club = await repository.GetClubById(id);
            if (club == null)
                return NotFound("No se ha encontrado un club que corresponda con el ID proporcionado");
            var response = srv.ObjectToDto(club);

            return Ok(response);
        }
    }
}