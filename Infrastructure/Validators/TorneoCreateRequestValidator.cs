using System;
using FluentValidation;
using clubs_api.Domain.Dtos.Requests;

namespace clubs_api.Infrastructure.Validators
{
    public class TorneoCreateRequestValidator : AbstractValidator<TorneoCreateRequest>
    {
        public TorneoCreateRequestValidator()
        {
            RuleFor(torneo => torneo.Nombre).NotEmpty();
            RuleFor(torneo => torneo.Disciplina).NotEmpty();
            RuleFor(torneo => torneo.NumeroEquipos).GreaterThanOrEqualTo(1);
            RuleFor(torneo => torneo.DisponibilidadLugares).GreaterThanOrEqualTo(1);
            RuleFor(torneo => torneo.CostoInscripcion).GreaterThanOrEqualTo(0);
            RuleFor(torneo => torneo.NumeroRondas).GreaterThanOrEqualTo(1);
        }
    }
}