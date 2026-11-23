using Market.Domain.Common;

namespace Market.Domain.Entities.Festival
{
    public class FestivalDayEntity : BaseEntity
    {
        
        public DateOnly Date { get; set; }
        public TimeOnly OpensAt { get; set; }
        public int Capacity { get; set; }
        public string? Program { get; set; }

        public static class Constraints
        {
            public const int ProgramMaxLength = 2000;
            public const int MinCapacity = 1;
            public const int MaxCapacity = 5000;

        }

    }

    
}
