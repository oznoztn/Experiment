using Experiment.Core.Entities;

namespace Experiment.Core.Repository;

public interface IInvoiceRepository
{
    Task<Invoice> GetInvoiceByIdAsync(Guid invoiceId);
}