using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace EXCSLA.Shared.Infrastructure.Data.EntityFrameworkCore.Config
{
    public static class CommonSqlProperties
    {
        public static void Build(EntityTypeBuilder builder)
        {
            builder.Property<DateTime>("CreatedDate")
                .HasDefaultValueSql("GetDate()");
            builder.Property<byte[]>("Version")
                .IsRowVersion();
        }
    }
}
