namespace Market.Application.Modules.Festival.FestivalDays.Commands.Update;

public class UpdateFestivalDayCommandHandler(IAppDbContext context)
: IRequestHandler<UpdateFestivalDayCommand, Unit>
{
    public async Task<Unit> Handle(UpdateFestivalDayCommand request, CancellationToken ct)
    {
        var day = await context.FestivalDays
            .Where(x => x.Id == request.Id)
            .FirstOrDefaultAsync(ct);

        if (day == null)
            throw new MarketNotFoundException($"Dan festivala (ID={request.Id}) nije pronadjen");

        if (await context.FestivalDays.AnyAsync(x => x.Id != request.Id && x.Date == request.Date, ct))
        {
            throw new MarketConflictException($"Dan festivala za datum {request.Date} vec postoji!");
        }

        day.Date = request.Date;
        day.OpensAt = request.OpensAt;
        day.Program = request.Program.Trim();
        day.Capacity = request.Capacity;


        await context.SaveChangesAsync(ct);

        return Unit.Value;
    }
}

