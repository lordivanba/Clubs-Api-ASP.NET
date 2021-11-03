using System;
using System.Collections.Generic;
using clubs_api.Domain.Entities;

namespace clubs_api.Domain.Dtos
{
    public record ClubDto(
        int Id,
        string Nombre,
        string Direccion,
        string Telefono,
        DateTime? FechaRegistro
    );
}