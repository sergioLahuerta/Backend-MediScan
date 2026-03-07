namespace MediScan.Core.Entities;

public class Plan
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int? MaxMessages { get; set; }
    public string? Features { get; set; }
}
