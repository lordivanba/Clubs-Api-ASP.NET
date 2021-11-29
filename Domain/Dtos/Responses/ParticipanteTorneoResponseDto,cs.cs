namespace clubs_api.Domain.Dtos.Responses
{
    public class ParticipanteTorneoResponseDto
    {
        public int Id { get; set; }
        public int TorneoId { get; set; }
        public string Torneo { get; set; }
        public int ClubId { get; set; }
        public string Club { get; set; }
    }
}