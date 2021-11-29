using clubs_api.Domain.Dtos.Requests;
using FluentValidation;

namespace clubs_api.Infrastructure.Validators
{
    public class ParticipanteTorneoCreateRequestValidator 
    : AbstractValidator<ParticipanteTorneoCreateRequest>
    {
        public ParticipanteTorneoCreateRequestValidator()
        {
            RuleFor(participante => participante.TorneoId).NotNull().GreaterThanOrEqualTo(1);
            RuleFor(participante => participante.ClubId).NotNull().GreaterThanOrEqualTo(1);
        }
    }
}