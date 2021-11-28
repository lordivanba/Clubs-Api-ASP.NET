using System;
using clubs_api.Domain.Dtos.Requests;
using FluentValidation;

namespace clubs_api.Infrastructure.Validators
{
    public class ClubUpdateRequestValidator : AbstractValidator<ClubUpdateRequest>
    {
        public ClubUpdateRequestValidator()
        {
            RuleFor(club => club.Nombre).NotEmpty();
            RuleFor(club => club.Telefono).Length(1,13);
        }
    }
}