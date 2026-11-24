

namespace Market.Application.Modules.Festival.FestivalDays.Queries.GetById;

    public class GetFestivalDayByIdQuery : IRequest<GetFestivalDayByIdQueryDto>
    {
    public int Id { get; set; }

    }

