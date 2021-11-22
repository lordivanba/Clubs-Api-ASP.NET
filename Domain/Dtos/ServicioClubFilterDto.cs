namespace clubs_api.Domain.Dtos
{
    public record ServicioClubFilterDto(
            string Disciplina,
            string Horario,
            int? PersonasPermitidas,
            int? RequiereEquipoEspecial,
            int? CapacidadesDiferentes
        );
}
