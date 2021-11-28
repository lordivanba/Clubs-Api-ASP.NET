using System;

namespace clubs_api.Domain.Dtos.Responses
{
    public class TorneoResponseDto
    {
        public int Id { get; set; }
        public string Disciplina { get; set; }
        public int? NumeroEquipos { get; set; }
        public int? DisponibilidadLugares { get; set; }
        public double? CostoInscripcion { get; set; }
        public string Bases { get; set; }
        public int? NumeroRondas { get; set; }
        public string TipoTorneo { get; set; }
        public DateTime? FechaRegistro { get; set; }
    };
}