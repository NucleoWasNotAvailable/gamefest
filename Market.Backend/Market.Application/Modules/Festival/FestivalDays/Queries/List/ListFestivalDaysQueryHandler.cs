namespace Market.Application.Modules.Festival.FestivalDays.Queries.List;

public sealed class ListFestivalDaysQueryHandler(IAppDbContext context)
: IRequestHandler<ListFestivalDaysQuery, PageResult<ListFestivalDaysQueryDto>>
{
    public async Task<PageResult<ListFestivalDaysQueryDto>> Handle(ListFestivalDaysQuery request, CancellationToken ct)
    {
        var query = context.FestivalDays.AsNoTracking();

        if (request.DateFrom is not null)
        {
            query = query.Where(x => x.Date >= request.DateFrom);
        }

        if (request.DateTo is not null)
        {
            query = query.Where(x => x.Date <= request.DateTo);
        }
        if (!string.IsNullOrWhiteSpace(request.Search))
        {
            query = query.Where(x => x.Program.Contains(request.Search));
        }


        var projectedQuery = query
            .OrderBy(x => x.Date)
            .Select(x => new ListFestivalDaysQueryDto
            {
                Id = x.Id,
                Date = x.Date,
                OpensAt = x.OpensAt,
                Capacity = x.Capacity,
                Program = x.Program
            });

        return await PageResult<ListFestivalDaysQueryDto>.FromQueryableAsync(projectedQuery, request.Paging, ct);
    }
}

