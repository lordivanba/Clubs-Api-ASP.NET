using System;
using System.Collections.Generic;

#nullable disable

namespace clubs_api.Domain.Entities
{
    public partial class ParticipanteTorneo
    {
        public int Id { get; set; }
        public int? TorneoId { get; set; }
        public int? ClubId { get; set; }

        public virtual Club Club { get; set; }
        public virtual Torneo Torneo { get; set; }
    }
}
