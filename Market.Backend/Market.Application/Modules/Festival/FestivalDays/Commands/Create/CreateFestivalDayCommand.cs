

namespace Market.Application.Modules.Festival.FestivalDays.Commands.Create
{
    public class CreateFestivalDayCommand : IRequest<int>
    {
        public required DateOnly Date { get; set; }
        public required TimeOnly OpensAt { get; set; }
        public required int Capacity { get; set; }
        public required string Program { get; set; }

        
    }
}
