using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernisationChallenge.Domain.TaskAggregate;

namespace ModernisationChallenge.Infrastructure.EntityConfigurations
{
    class TaskEntityTypeConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(o => o.Id);

            builder.Property(b => b.DateCreated)
                .IsRequired();

            builder.Property(b => b.DateModified)
                .IsRequired();

            builder.Property(b => b.DateDeleted);

            builder.Property(b => b.Completed)
                .IsRequired();

            builder.Property(b => b.Details)
                .IsRequired();
        }
    }
}
