
using Market.Domain.Entities.Festival;

namespace Market.Application.Modules.Festival.FestivalDays.Commands.Create;

public class CreateFestivalDayCommandHandler(IAppDbContext context) 
    : IRequestHandler<CreateFestivalDayCommand, int>
{
    public async Task<int> Handle(CreateFestivalDayCommand request, CancellationToken ct)
    {
        if(await context.FestivalDays.AnyAsync(x=> x.Date == request.Date, ct))
        {
            throw new MarketConflictException($"Dan festivala za datum {request.Date} vec postoji!");
        }


        var festivalDay = new FestivalDayEntity
        {
            Date = request.Date,
            OpensAt = request.OpensAt,
            Program = request.Program.Trim(),
            Capacity = request.Capacity
        };

        context.FestivalDays.Add(festivalDay);
        await context.SaveChangesAsync(ct);

        return festivalDay.Id;
    }

}

