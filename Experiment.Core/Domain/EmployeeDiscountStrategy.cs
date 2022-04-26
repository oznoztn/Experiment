using Experiment.Core.Entities;
using Experiment.Core.Enums;
using Experiment.Core.QueryExtensions;

namespace Experiment.Core.Domain;

public class EmployeeDiscountStrategy : IDiscountCalculationStrategy
{
    public Discount Calculate(User user, Invoice userInvoice)
    {
        if (user.UserType != UserType.Employee)
            return new Discount() { Amount = 0, Title = nameof(EmployeeDiscountStrategy)};

        decimal nonGroceryCost = userInvoice.Products.SumNonGroceriesTotalCost();
        
        decimal discount = nonGroceryCost * 0.30M;
        
        return new Discount
        {
            Amount = discount,
            Title = nameof(EmployeeDiscountStrategy),
        };
    }
}