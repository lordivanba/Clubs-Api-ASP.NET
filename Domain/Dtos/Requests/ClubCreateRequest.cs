using System;

namespace clubs_api.Domain.Dtos.Requests
{
    public class ClubCreateRequest 
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}