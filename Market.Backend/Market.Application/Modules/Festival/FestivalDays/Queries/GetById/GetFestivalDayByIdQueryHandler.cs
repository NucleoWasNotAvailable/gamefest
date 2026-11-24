
namespace Market.Application.Modules.Festival.FestivalDays.Queries.GetById;

public class GetFestivalDayByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetFestivalDayByIdQuery, GetFestivalDayByIdQueryDto>
{
    public async Task<GetFestivalDayByIdQueryDto> Handle(GetFestivalDayByIdQuery request, CancellationToken ct)
    {
        var festivalDay = await context.FestivalDays
            .Where(x => x.Id == request.Id)
            .Select(c => new GetFestivalDayByIdQueryDto
            {
                Id = c.Id,
                Date = c.Date,
                Program = c.Program,
                Capacity = c.Capacity,
                OpensAt = c.OpensAt
            }).FirstOrDefaultAsync(ct);

        if(festivalDay == null)
        {
            throw new MarketNotFoundException($"Fetsivalski dan (ID={request.Id}) nije pronadjen");
        }
        return festivalDay;
    }
}

