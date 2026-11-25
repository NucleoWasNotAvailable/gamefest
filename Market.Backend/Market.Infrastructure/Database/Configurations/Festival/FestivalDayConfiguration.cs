using Market.Domain.Entities.Festival;


namespace Market.Infrastructure.Database.Configurations.Festival
{
    public class FestivalDayConfiguration : IEntityTypeConfiguration<FestivalDayEntity>
    {
        public void Configure(EntityTypeBuilder<FestivalDayEntity> builder)
        {
            builder
                .ToTable("FestivalDays");

            builder
                .Property(x => x.Program)
                .IsRequired()
                .HasMaxLength(FestivalDayEntity.Constraints.ProgramMaxLength);

            builder
             .HasIndex(x => x.Date)
             .IsUnique()
             .HasFilter("[IsDeleted]=0");

        }
    }
}
