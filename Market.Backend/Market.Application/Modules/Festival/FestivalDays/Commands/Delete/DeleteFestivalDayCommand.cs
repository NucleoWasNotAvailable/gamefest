namespace Market.Application.Modules.Festival.FestivalDays.Commands.Delete
{
    public class DeleteFestivalDayCommand : IRequest<Unit>
    {
        public required int Id {  get; set; }

    }
}
