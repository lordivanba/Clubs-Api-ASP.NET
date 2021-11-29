using System;
using System.Collections.Generic;

#nullable disable

namespace clubs_api.Domain.Entities
{
    public partial class Torneo
    {
        public Torneo()
        {
            ParticipanteTorneos = new HashSet<ParticipanteTorneo>();
        }

        public int Id { get; set; }
        public string Disciplina { get; set; }
        public int? NumeroEquipos { get; set; }
        public int? DisponibilidadLugares { get; set; }
        public double? CostoInscripcion { get; set; }
        public string Bases { get; set; }
        public int? NumeroRondas { get; set; }
        public string TipoTorneo { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public string Resultado { get; set; }
        public string Estatus { get; set; }
        public string Nombre { get; set; }

        public virtual ICollection<ParticipanteTorneo> ParticipanteTorneos { get; set; }
    }
}
