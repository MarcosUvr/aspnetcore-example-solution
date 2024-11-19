namespace Empresa.Proyecto.Core.Entities
{
    public class NewEntity : BaseEntity
    {
        public string Name { get; set; } = null!;
        public SimpleEntity Option { get; set; } = null!;
    }
}
