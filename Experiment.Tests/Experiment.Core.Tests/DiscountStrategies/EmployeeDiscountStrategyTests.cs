using System.Collections.Generic;
using Experiment.Core.Domain;
using Experiment.Core.Entities;
using Experiment.Core.Enums;
using FluentAssertions;
using Xunit;

namespace Experiment.Tests.Experiment.Core.Tests.DiscountStrategies;

public class EmployeeDiscountStrategyTests
{
    [Fact]
    public void ShouldApplyDiscountOf30Percent_IfTheUserIsAnEmployee()
    {
        // ARRANGE
        var user = new User()
        {
            UserType = UserType.Employee
        };

        Invoice invoice = new()
        {
            Products = new List<Product>()
            {
                new Product() { ProductType = ProductType.NonGrocery, Price = 100},
            }
        };

        // ACT
        var strategy = new EmployeeDiscountStrategy();
        var discount = strategy.Calculate(user, invoice);

        // ASSERT
        discount.Amount.Should().Be(30);
        discount.Title.Should().Be(nameof(EmployeeDiscountStrategy));
    }

    [Fact]
    public void ShouldNotApplyDiscountOf10Percent_IfTheUserIsNotAnAffiliate()
    {
        // ARRANGE
        var user = new User()
        {
            UserType = UserType.Undefined
        };

        Invoice invoice = new()
        {
            Products = new List<Product>()
            {
                new Product() { ProductType = ProductType.NonGrocery, Price = 100},
            }
        };

        // ACT
        var strategy = new EmployeeDiscountStrategy();
        var discount = strategy.Calculate(user, invoice);

        // ASSERT
        discount.Amount.Should().Be(0);
        discount.Title.Should().Be(nameof(EmployeeDiscountStrategy));
    }
}