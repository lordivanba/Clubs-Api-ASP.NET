using AutoMapper;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Dtos.Requests;
using clubs_api.Domain.Dtos.Responses;
using clubs_api.Domain.Entities;

namespace clubs_api.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //CLUBS
            CreateMap<Club, ClubResponseDto>();
            CreateMap<ClubCreateRequest, Club>();
            CreateMap<ClubUpdateRequest, Club>();
            CreateMap<ClubFilterDto, Club>();

            //SERVICIOS
            CreateMap<ServicioClub, ServicioClubResponseDto>()
            .ForMember(dest => dest.Club, opt => opt.MapFrom(src => src.Club.Nombre));
            CreateMap<ServicioClubCreateRequest, ServicioClub>();
            CreateMap<ServicioClubUpdateRequest, ServicioClub>();
            CreateMap<ServicioClubFilterDto, ServicioClub>();

            //TORNEOS
            CreateMap<Torneo, TorneoResponseDto>();
            CreateMap<TorneoCreateRequest, Torneo>();
            CreateMap<TorneoUpdateRequest, Torneo>();
            CreateMap<TorneoFilterDto, Torneo>();

            //PARTICIPANTES
            CreateMap<ParticipanteTorneo, ParticipanteTorneoResponseDto>()
            .ForMember(dest => dest.Torneo, opt => opt.MapFrom(src => src.Torneo.Nombre))
            .ForMember(dest => dest.Club, opt => opt.MapFrom(src => src.Club.Nombre));
            CreateMap<ParticipanteTorneoCreateRequest, ParticipanteTorneo>();
        }
    }
}