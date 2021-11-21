using clubs_api.Domain.Dtos;
using clubs_api.Domain.Entities;
using System.Collections.Generic;

namespace clubs_api.Domain.Interfaces
{
    public interface IClubService
    {
        ClubDto ObjectToDto(Club club);
        IEnumerable<ClubDto> ObjectsToDtos(IEnumerable<Club> clubs);
        Club DtoToObject(ClubFilterDto dto);

        bool ValidateCreate(Club club);
        bool ValidateUpdate(Club club);
    }
}
