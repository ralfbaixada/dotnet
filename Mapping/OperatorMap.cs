using Bases.Bases;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mapping
{
    public class OperatorMap : IEntityTypeConfiguration<Operator>
    {
        public void Configure(EntityTypeBuilder<Operator> builder)
        {
            BaseEntityMap.Configure<Operator, int>(builder);
            builder.ToTable("operator", "app_operator");
            // primarikey
            builder.HasKey(x => x.Id);
            //foreignkey
            builder.HasMany(x => x.OperatorRoles).WithOne(x => x.Operator).HasForeignKey(x => x.OperatorId);
        }
    }
}
