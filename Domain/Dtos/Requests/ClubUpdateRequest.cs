
namespace clubs_api.Domain.Dtos.Requests
{
    public class ClubUpdateRequest
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
    }
}