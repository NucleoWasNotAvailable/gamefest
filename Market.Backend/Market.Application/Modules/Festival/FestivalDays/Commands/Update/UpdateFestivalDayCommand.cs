namespace Market.Application.Modules.Festival.FestivalDays.Commands.Update
{
    public sealed class UpdateFestivalDayCommand : IRequest<Unit>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public required DateOnly Date { get; set; }

        public required TimeOnly OpensAt { get; set; }
        public required int Capacity { get; set; }
        public required string Program { get; set; }
    }
}
