using System.Collections.Generic;
using System.Linq;
using clubs_api.Domain.Dtos;
using clubs_api.Domain.Entities;
using clubs_api.Domain.Interfaces;

namespace clubs_api.Application.Services
{
    public class TorneoService : ITorneoService
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

        public Torneo DtoToObject(TorneoFilterDto dto)
        {
            var torneo = new Torneo
            {
                Id = 0,
                Disciplina = dto.Disciplina,
                NumeroEquipos = dto.NumeroEquipos,
                DisponibilidadLugares = dto.DisponibilidadLugares,
                CostoInscripcion = dto.CostoInscripcion,
                Bases = dto.Bases,
                NumeroRondas = dto.NumeroRondas,
                TipoTorneo = dto.TipoTorneo,
                Resultado = dto.Resultado,
                Estatus = dto.Estatus,
                FechaRegistro = dto.FechaRegistro
            };

            return torneo;
        }

        public bool ValidateCreate(Torneo torneo) 
        {
            if (string.IsNullOrEmpty(torneo.Disciplina))
                return false;

            if (torneo.CostoInscripcion < 0)
                return false;

            return true;
        }

        public bool ValidateUpdate(Torneo torneo)
        {
            if (torneo.Id <= 0)
                return false;

            if (string.IsNullOrEmpty(torneo.Disciplina))
                return false;

            if (torneo.CostoInscripcion < 0)
                return false;

            return true;
        }
    }
}