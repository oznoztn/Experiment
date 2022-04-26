using Experiment.Core.Domain;
using Experiment.Core.Entities;
using Experiment.Core.Enums;
using Experiment.Core.Infrastructure;

namespace Experiment.Core.Factory;

public class CalculationStrategyFactory
{
    public virtual IDiscountCalculationStrategy CreateForEvery100DollarsDiscountStrategy(User user, Invoice invoice)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));

        if (invoice == null)
            throw new ArgumentNullException(nameof(invoice));

        return new ForEvery100DollarsDiscountStrategy();
    }

    public virtual IDiscountCalculationStrategy Create(User user, Invoice invoice)
    {
        if(user == null)
            throw new ArgumentNullException(nameof(user));

        if (invoice == null)
            throw new ArgumentNullException(nameof(invoice));

        if (user.UserType == UserType.Employee)
            return new EmployeeDiscountStrategy();

        if (user.UserType == UserType.Affiliate)
            return new AffiliateDiscountStrategy();

        if (user.UserType == UserType.Customer)
            return new CustomerDiscountStrategy(new SystemClock());

        return new NullDiscountStrategy();
    }
}