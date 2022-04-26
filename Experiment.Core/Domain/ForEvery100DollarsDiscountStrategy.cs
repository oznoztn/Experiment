using Experiment.Core.Entities;

namespace Experiment.Core.Domain;

public class ForEvery100DollarsDiscountStrategy : IDiscountCalculationStrategy
{
    public Discount Calculate(User user, Invoice userInvoice)
    {
        decimal totalCost = userInvoice.Products.Sum(t => t.Price);
        if (totalCost < 100)
        {
            return new Discount()
            {
                Amount = 0,
                Title = nameof(ForEvery100DollarsDiscountStrategy)
            };
        }

        decimal discount = ((totalCost - (totalCost % 100)) / 100) * 5;
        return new Discount()
        {
            Amount = discount, 
            Title = nameof(ForEvery100DollarsDiscountStrategy)
        };
    }
}