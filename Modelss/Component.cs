namespace Cwiczenia7.Modelss;

public class Component
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public int ComponentManufacturerId { get; set; }
    public ComponentManufacturer ComponentManufacturer { get; set; } = null!;

    public int ComponentTypeId { get; set; }
    public ComponentType ComponentType { get; set; } = null!;

    public ICollection<PcComponent> PcComponents { get; set; } = new List<PcComponent>();
}