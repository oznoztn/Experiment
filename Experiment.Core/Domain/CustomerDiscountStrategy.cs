using Experiment.Core.Entities;
using Experiment.Core.Enums;
using Experiment.Core.Infrastructure;
using Experiment.Core.QueryExtensions;

namespace Experiment.Core.Domain;

public class CustomerDiscountStrategy : IDiscountCalculationStrategy
{
    private readonly IClock _clock;

    public CustomerDiscountStrategy(IClock clock)
    {
        _clock = clock;
    }

    public Discount Calculate(User user, Invoice userInvoice)
    {
        if (user.UserType != UserType.Customer)
            return new Discount() { Amount = 0, Title = nameof(EmployeeDiscountStrategy) };

        decimal nonGroceryCost = userInvoice.Products.SumNonGroceriesTotalCost();

        decimal discount = default;
        if (user.RegisterationDate.AddYears(2) < _clock.GetCurrentDateTimeUtc())
            discount = nonGroceryCost * 0.05M;
        
        return new Discount
        {
            Amount = discount,
            Title = nameof(CustomerDiscountStrategy)
        };
    }
}