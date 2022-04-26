using Experiment.Core.Entities;

namespace Experiment.Core.Domain;

public class NullDiscountStrategy : IDiscountCalculationStrategy
{
    public Discount Calculate(User user, Invoice userInvoice)
    {
        return new Discount()
        {
            Amount = 0,
            Title = nameof(NullDiscountStrategy)
        };
    }
}