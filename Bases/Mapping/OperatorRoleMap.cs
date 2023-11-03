using Bases.Bases;
using Bases.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Mapping
{
    public class OperatorRolesMap : IEntityTypeConfiguration<OperatorRole>
    {
        public void Configure(EntityTypeBuilder<OperatorRole> builder)
        {
            BaseEntityMap.Configure<OperatorRole, int>(builder);
            builder.ToTable("operatorRoles", "app_operator_roles");
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Operator)
            .WithMany(x => x.OperatorRoles)
            .HasForeignKey(x => x.OperatorId);
        }
    }
}