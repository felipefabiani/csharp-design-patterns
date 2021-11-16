using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SpecificationDesignPattern.Logic.Movies;
public class DirectorEntityTypeConfiguration : IEntityTypeConfiguration<DirectorEntity>
{
    public void Configure(EntityTypeBuilder<DirectorEntity> builder)
    {
        builder
            .ToTable("Director");
        builder
            .HasKey(d => d.Id);
        builder
            .Property(m => m.Name)
            .HasMaxLength(50)
            .IsUnicode(false)
            .IsRequired();
    }
}