using System;
using clubs_api.Domain.Dtos.Requests;
using FluentValidation;

namespace clubs_api.Infrastructure.Validators
{
    public class ServicioClubCreateRequestValidator : AbstractValidator<ServicioClubCreateRequest>
    {
        public ServicioClubCreateRequestValidator()
        {
            RuleFor(servicio => servicio.Disciplina).NotEmpty();
            RuleFor(servicio => servicio.PersonasPermitidas).GreaterThan(1);
            RuleFor(servicio => servicio.CapacidadesDiferentes).LessThanOrEqualTo(1).GreaterThanOrEqualTo(0);
            RuleFor(servicio => servicio.RequiereEquipoEspecial).LessThanOrEqualTo(1).GreaterThanOrEqualTo(0);
            RuleFor(servicio => servicio.ClubId).NotNull();
        }
    }
}