using Microsoft.EntityFrameworkCore;
using Empresa.Proyecto.Core.Entities;


namespace Empresa.Proyecto.Infra.Data
{
    public class MyProjectContext : DbContext
    {
        /// <summary>
        /// Constructor para DI
        /// </summary>
        /// <param name="options"></param>
        public MyProjectContext(DbContextOptions<MyProjectContext> options) : base(options)
        {

        }

        public DbSet<SimpleEntity> SimpleEntity => Set<SimpleEntity>();
        public DbSet<ComplexEntity> ComplexEntity => Set<ComplexEntity>();

        protected override void OnModelCreating(ModelBuilder builder)
        {           
            builder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Método para inicializar el Seed en la base de datos
        /// </summary>
        public void Seed()
        {
            if (!SimpleEntity.Any())
            {
                SimpleEntity.AddRange(new[]
                {
                    new SimpleEntity { Name = "Nuevo", Value = "1", Created = DateTime.UtcNow, Modified = DateTime.UtcNow },
                    new SimpleEntity { Name = "Existente", Value = "2", Created = DateTime.UtcNow, Modified = DateTime.UtcNow },
                    new SimpleEntity { Name = "Reconstruido", Value = "3", Created = DateTime.UtcNow, Modified = DateTime.UtcNow }
                    });

                SaveChanges();
            }
        }

    }
}
