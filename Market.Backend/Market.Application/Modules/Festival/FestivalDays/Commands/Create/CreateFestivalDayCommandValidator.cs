

using Market.Domain.Entities.Festival;

namespace Market.Application.Modules.Festival.FestivalDays.Commands.Create;

public sealed class CreateFestivalDayCommandValidator : AbstractValidator<CreateFestivalDayCommand>
{
    public CreateFestivalDayCommandValidator()
    {

        RuleFor(x => x.Date).NotEmpty().WithMessage("Datum je obavezan!")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Datum festivala ne moze biti manji od danasnjeg datuma!");

        RuleFor(x => x.Capacity)
            .InclusiveBetween(FestivalDayEntity.Constraints.MinCapacity, FestivalDayEntity.Constraints.MaxCapacity) // the capacity of the festival must be between the minimum and maximum numbers that are set in the entity FestivalDay
            .WithMessage($"Kapacitet festivala mora biti izmedju {FestivalDayEntity.Constraints.MinCapacity} i {FestivalDayEntity.Constraints.MaxCapacity}!");

        RuleFor(x => x.Program)
            .NotEmpty().WithMessage("Opis programa je obavezan!")
            .MaximumLength(FestivalDayEntity.Constraints.ProgramMaxLength)
            .WithMessage($"Opis programa moze imati najvise {FestivalDayEntity.Constraints.ProgramMaxLength} znakova!");

    }
}

