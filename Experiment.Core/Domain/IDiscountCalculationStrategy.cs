using Experiment.Core.Entities;

namespace Experiment.Core.Domain;

public interface IDiscountCalculationStrategy
{
    Discount Calculate(User user, Invoice userInvoice);
}