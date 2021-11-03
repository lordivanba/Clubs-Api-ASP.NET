using System.Collections.Generic;
using System.Linq;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Entities;

namespace clubs_api.Application.Services
{
    public class TorneoToDtoService
    {
        public TorneoDto ObjectToDto(Torneo torneo)
        {
            return new TorneoDto(
                Id: torneo.Id,
                Disciplina: torneo.Disciplina,
                NumeroEquipos: torneo.NumeroEquipos,
                DisponibilidadLugares: torneo.DisponibilidadLugares,
                CostoInscripcion: torneo.CostoInscripcion,
                Bases: torneo.Bases,
                NumeroRondas: torneo.NumeroRondas,
                TipoTorneo: torneo.TipoTorneo,
                Resultado: torneo.Resultado,
                Estatus: torneo.Estatus,
                FechaRegistro: torneo.FechaRegistro
            );
        }

        public IEnumerable<TorneoDto> ObjectsToDtos(IEnumerable<Torneo> torneos)
        {
            return torneos.Select(torneo => new TorneoDto(
                Id: torneo.Id,
                Disciplina: torneo.Disciplina,
                NumeroEquipos: torneo.NumeroEquipos,
                DisponibilidadLugares: torneo.DisponibilidadLugares,
                CostoInscripcion: torneo.CostoInscripcion,
                Bases: torneo.Bases,
                NumeroRondas: torneo.NumeroRondas,
                TipoTorneo: torneo.TipoTorneo,
                Resultado: torneo.Resultado,
                Estatus: torneo.Estatus,
                FechaRegistro: torneo.FechaRegistro
            ));
        }
    }
}