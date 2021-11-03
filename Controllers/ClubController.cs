using clubs_api.Application.Services;
using clubs_api.Domain.Dtos;
using clubs_api.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace clubs_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClubController : ControllerBase
    {
        [HttpGet]
        [Route("getClubs")]
        public IActionResult GetClubs()
        {   
            var repository = new ClubSqlRepository();
            var srv = new ClubToDtoService();
            var clubs = repository.GetClubs();
            var response = srv.ObjectsToDtos(clubs);
            return Ok(response);
        }

        [HttpGet]
        [Route("getClub/{id::int}")]
        public IActionResult GetClubById(int id)
        {
            var repository = new ClubSqlRepository();
            var srv = new ClubToDtoService();
            var club = repository.GetClubById(id);
            var response = srv.ObjectToDto(club);

            return Ok(response);
        }

        
        // public IActionResult GetClubs()
        // {   
        //     var repository = new ClubSqlRepository();
        //     var srv = new ObjectToDtoService();
        //     var clubs = repository.GetClubs().Select(club => new ClubDto(
        //         Id: club.Id,
        //         Nombre: club.Nombre,
        //         Direccion: club.Direccion,
        //         Telefono: club.Telefono,
        //         FechaRegistro: club.FechaRegistro
        //     ));
        //     return Ok(clubs);
        // }
    }
}