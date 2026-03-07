using System.Collections.Generic;
using System.Threading.Tasks;
using MediScan.Core.Entities;

namespace MediScan.Core.Interfaces.Services;

public interface IDetailInvoiceService
{
    Task<IEnumerable<DetailInvoice>> GetAllAsync();
    Task<DetailInvoice?> GetByIdAsync(object id);
    Task<DetailInvoice> AddAsync(DetailInvoice entity);
    Task UpdateAsync(DetailInvoice entity);
    Task DeleteAsync(DetailInvoice entity);
}

