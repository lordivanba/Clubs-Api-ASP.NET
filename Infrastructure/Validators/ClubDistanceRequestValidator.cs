using clubs_api.Domain.Dtos.Requests;
using FluentValidation;

namespace clubs_api.Infrastructure.Validators
{
    public class ClubDistanceRequestValidator : AbstractValidator<ClubDistanceRequest>
    {
        public ClubDistanceRequestValidator()
        {
            RuleFor(club => club.CoordenadaX).NotNull();
            RuleFor(club => club.CoordenadaY).NotNull();
        }
    }
}