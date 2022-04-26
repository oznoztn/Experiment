
using Experiment.Core.Entities;
using Experiment.Core.Enums;

namespace Experiment.Service.DTOs
{
    public class InvoiceSummaryDto
    {
        public decimal Cost { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalCost { get; set; }
        public List<Discount> AppliedDiscounts { get; set; }
        public string UserType { get; set; }
        public Guid UserId { get; set; }
    }
}
