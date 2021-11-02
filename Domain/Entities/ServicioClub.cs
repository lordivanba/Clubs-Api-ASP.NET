using System;
using System.Collections.Generic;

#nullable disable

namespace clubs_api.Domain.Entities
{
    public partial class ServicioClub
    {
        public int Id { get; set; }
        public string Disciplina { get; set; }
        public string Horario { get; set; }
        public int? PersonasPermitidas { get; set; }
        public int? RequiereEquipoEspecial { get; set; }
        public int? CapacidadesDiferentes { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public int? ClubId { get; set; }

        public virtual Club Club { get; set; }
    }
}
