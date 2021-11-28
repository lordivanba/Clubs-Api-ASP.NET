using System;
using System.Collections.Generic;
using clubs_api.Domain.Entities;

namespace clubs_api.Domain.Dtos.Responses
{
    public class ClubResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaRegistro { get; set; }
    }
}