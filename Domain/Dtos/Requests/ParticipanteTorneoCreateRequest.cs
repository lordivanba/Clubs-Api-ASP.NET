namespace clubs_api.Domain.Dtos.Requests
{
    public class ParticipanteTorneoCreateRequest
    {
        public int Id { get; set; }
        public int TorneoId { get; set; }
        public int ClubId { get; set; }
    }
}