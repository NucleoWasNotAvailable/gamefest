

namespace Market.Application.Modules.Festival.FestivalDays.Queries.List;

public sealed class ListFestivalDaysQueryDto
{
    public required int Id { get; init; }
    public required DateOnly Date { get; init; }
    public required TimeOnly OpensAt { get; init; }
    public required int Capacity { get; init; }
    public required string Program { get; init; }

}

