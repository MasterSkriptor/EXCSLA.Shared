using EXCSLA.Shared.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EXCSLA.Shared.Infrastructure.Data.EntityFrameworkCore.Config
{
    public abstract class AggregateRootTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : AggregateRoot
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Attach sql properties
            CommonSqlProperties.Build(builder);

            // Ignore Aggregate Events
            builder.Ignore(c => c.Events);
        }
    }
}
