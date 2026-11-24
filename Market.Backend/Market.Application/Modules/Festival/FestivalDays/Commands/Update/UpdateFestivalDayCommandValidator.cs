
using Market.Domain.Entities.Festival;

namespace Market.Application.Modules.Festival.FestivalDays.Commands.Update;

    public class UpdateFestivalDayCommandValidator : AbstractValidator<UpdateFestivalDayCommand>
    {
    public UpdateFestivalDayCommandValidator()
    {
        RuleFor(x => x.Id).GreaterThan(0);

        RuleFor(x => x.Date).NotEmpty().WithMessage("Datum je obavezan!")
            .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
            .WithMessage("Datum festivala ne moze biti manji od danasnjeg datuma!");

        RuleFor(x => x.Capacity)
            .InclusiveBetween(FestivalDayEntity.Constraints.MinCapacity, FestivalDayEntity.Constraints.MaxCapacity) 
            .WithMessage($"Kapacitet festivala mora biti izmedju {FestivalDayEntity.Constraints.MinCapacity} i {FestivalDayEntity.Constraints.MaxCapacity}!");

        RuleFor(x => x.Program)
            .NotEmpty().WithMessage("Opis programa je obavezan!")
            .MaximumLength(FestivalDayEntity.Constraints.ProgramMaxLength)
            .WithMessage($"Opis programa moze imati najvise {FestivalDayEntity.Constraints.ProgramMaxLength} znakova!");
    }
    }

