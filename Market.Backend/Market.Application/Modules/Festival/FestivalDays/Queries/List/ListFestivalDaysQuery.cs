

namespace Market.Application.Modules.Festival.FestivalDays.Queries.List;

public sealed class ListFestivalDaysQuery : BasePagedQuery<ListFestivalDaysQueryDto>
{
    public DateOnly? DateFrom {  get; init; }
    public DateOnly? DateTo { get; init; }

    public string? Search {  get; init; }

}

