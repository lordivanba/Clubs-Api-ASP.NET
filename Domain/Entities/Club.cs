using System;
using System.Collections.Generic;

#nullable disable

namespace clubs_api.Domain.Entities
{
    public partial class Club
    {
        public Club()
        {
            ServicioClubs = new HashSet<ServicioClub>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public DateTime? FechaRegistro { get; set; }

        public virtual ICollection<ServicioClub> ServicioClubs { get; set; }
    }
}
