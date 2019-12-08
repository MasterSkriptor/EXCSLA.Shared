using EXCSLA.Shared.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EXCSLA.Shared.Infrastructure.Data.EntityFrameworkCore.Config
{
    public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            // Attach sql properties
            CommonSqlProperties.Build(builder);
        }
    }
}
