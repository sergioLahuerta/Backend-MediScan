namespace MediScan.Core.Entities;

public class DetailInvoice
{
    public int Id { get; set; }
    public int InvoiceId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Subtotal { get; set; }

    public Invoice Invoice { get; set; } = null!;
}
