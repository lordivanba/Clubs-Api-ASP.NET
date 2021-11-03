using System;

namespace clubs_api.Domain.Dtos
{
    public record TorneoDto(
        int Id,
        string Disciplina,
        int? NumeroEquipos,
        int? DisponibilidadLugares,
        double? CostoInscripcion,
        string Bases,
        int? NumeroRondas,
        string TipoTorneo,
        string Resultado,
        string Estatus,
        DateTime? FechaRegistro
    );
}