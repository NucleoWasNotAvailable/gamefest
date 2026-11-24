

namespace Market.Application.Modules.Festival.FestivalDays.Queries.GetById;

public class GetFestivalDayByIdQueryValidator : AbstractValidator<GetFestivalDayByIdQuery>
{
    public GetFestivalDayByIdQueryValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id mora biti pozitivan broj.");
    }
}

