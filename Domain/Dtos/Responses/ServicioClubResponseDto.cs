using System;
using clubs_api.Domain.Entities;

namespace clubs_api.Domain.Dtos.Responses
{
    public class ServicioClubResponseDto
    {
        public int Id { get; set; }
        public string Disciplina { get; set; }
        public string Horario { get; set; }
        public int? PersonasPermitidas { get; set; }
        public int? RequiereEquipoEspecial { get; set; }
        public int? CapacidadesDiferentes { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int ClubId { get; set; }
        public string Club { get; set; }
    };
}