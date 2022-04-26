using Experiment.Core.Entities;
using Experiment.Core.Enums;
using Experiment.Core.Infrastructure;
using Experiment.Core.QueryExtensions;

namespace Experiment.Core.Domain;

public class AffiliateDiscountStrategy : IDiscountCalculationStrategy
{
    public Discount Calculate(User user, Invoice userInvoice)
    {
        if (user.UserType != UserType.Affiliate)
            return new Discount() {Amount = 0, Title = nameof(AffiliateDiscountStrategy)};

        decimal nonGroceryCost = userInvoice.Products.SumNonGroceriesTotalCost();

        decimal discount = nonGroceryCost * 0.10M;

        return new Discount()
        {
            Amount = discount,
            Title = nameof(AffiliateDiscountStrategy)
        };
    }
}