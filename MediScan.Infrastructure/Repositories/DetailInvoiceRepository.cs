using MediScan.Core.Entities;
using MediScan.Core.Interfaces.Repositories;
using MediScan.Infrastructure.Data;

namespace MediScan.Infrastructure.Repositories;

public class DetailInvoiceRepository : Repository<DetailInvoice>, IDetailInvoiceRepository
{
    public DetailInvoiceRepository(MediScanDbContext context) : base(context) { }
}

