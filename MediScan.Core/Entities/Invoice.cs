namespace MediScan.Core.Entities;

public class Invoice
{
    public int Id { get; set; }
    public int PaymentId { get; set; }
    public string InvoiceNumber { get; set; } = string.Empty;
    public DateTime InvoiceDate { get; set; }
    public decimal TotalAmount { get; set; }

    public Payment Payment { get; set; } = null!;
}
