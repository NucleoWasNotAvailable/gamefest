namespace Market.Application.Modules.Festival.FestivalDays.Commands.Delete
{
    public class DeleteFestivalDayCommandHandler(IAppDbContext context , IAppCurrentUser appCurrentUser)
        :IRequestHandler<DeleteFestivalDayCommand,Unit>
    {
        
        public async Task<Unit> Handle(DeleteFestivalDayCommand request, CancellationToken ct)
        {
            if (!appCurrentUser.IsAdmin)
                throw new MarketForbiddenException("Samo admin moze obrisati dan festivala.");

            var day = await context.FestivalDays
                .FirstOrDefaultAsync(x=> x.Id == request.Id, ct);


            if (day == null)
                throw new MarketNotFoundException($"Dan festivala (ID={request.Id}) nije pronadjen");



            // TODO: when TicketEntity exists, block deleting a day that has sold tickets (409).
            

            context.FestivalDays.Remove(day);
            await context.SaveChangesAsync(ct);

            return Unit.Value;
        }

    }
}
