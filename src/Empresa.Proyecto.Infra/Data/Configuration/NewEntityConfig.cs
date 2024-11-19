using Empresa.Proyecto.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Empresa.Proyecto.Infra.Data.Configuration
{
    internal class NewEntityConfig : IEntityTypeConfiguration<NewEntity>
    {
        public void Configure(EntityTypeBuilder<NewEntity> builder)
        {
            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
