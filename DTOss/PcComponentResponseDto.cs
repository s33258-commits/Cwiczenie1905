namespace Cwiczenia7.DTOss;

public class PcComponentResponseDto
{
    public string ComponentCode { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public int Amount { get; set; }
}