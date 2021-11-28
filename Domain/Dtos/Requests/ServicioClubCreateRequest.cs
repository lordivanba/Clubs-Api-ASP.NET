using System;

namespace clubs_api.Domain.Dtos.Requests
{
    public class ServicioClubCreateRequest
    {
        public string Disciplina { get; set; }
        public string Horario { get; set; }
        public int? PersonasPermitidas { get; set; }
        public int? RequiereEquipoEspecial { get; set; }
        public int? CapacidadesDiferentes { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? ClubId { get; set; }
    }
}