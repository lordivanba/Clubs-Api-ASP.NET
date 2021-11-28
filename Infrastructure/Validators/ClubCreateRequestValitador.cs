using clubs_api.Domain.Dtos.Requests;
using FluentValidation;

namespace clubs_api.Infrastructure.Validators
{
    public class ClubCreateRequestValidator : AbstractValidator<ClubCreateRequest>
    {
        public ClubCreateRequestValidator()
        {
            RuleFor(club => club.Nombre).NotEmpty();
            RuleFor(club => club.Telefono).Length(1,13);
        }
    }
}