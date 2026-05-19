namespace Cwiczenia7.DTOss;
using System.ComponentModel.DataAnnotations;

public class UpdatePcDto
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = null!;

    [Range(0.1, double.MaxValue)]
    public double Weight { get; set; }

    [Range(1, 120)]
    public int Warranty { get; set; }

    public DateTime CreatedAt { get; set; }

    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}
