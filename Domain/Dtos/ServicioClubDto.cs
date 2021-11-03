using System;
using clubs_api.Domain.Entities;

namespace clubs_api.Domain.Dtos
{
    public record ServicioClubDto(
        int Id,
        string Disciplina,
        string Horario,
        int? PersonasPermitidas,
        int? RequiereEquipoEspecial,
        int? CapacidadesDiferentes,
        DateTime? FechaRegistro
    );
}