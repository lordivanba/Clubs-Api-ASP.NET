namespace clubs_api.Domain.Dtos.Requests
{
    public class ServicioClubUpdateRequest
    {
        public int Id { get; set; }
        public string Disciplina { get; set; }
        public string Horario { get; set; }
        public int? PersonasPermitidas { get; set; }
        public int? RequiereEquipoEspecial { get; set; }
        public int? CapacidadesDiferentes { get; set; }
        public int? ClubId { get; set; }
    }
}