using System;
using System.Collections.Generic;

#nullable disable

namespace clubs_api.Domain.Entities
{
    public partial class Club
    {
        public Club()
        {
            ParticipanteTorneos = new HashSet<ParticipanteTorneo>();
            ServicioClubs = new HashSet<ServicioClub>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public double? CoordenadaX { get; set; }
        public double? CoordenadaY { get; set; }
        public string Horario { get; set; }
        public string Logo { get; set; }

        public virtual ICollection<ParticipanteTorneo> ParticipanteTorneos { get; set; }
        public virtual ICollection<ServicioClub> ServicioClubs { get; set; }
    }
}
