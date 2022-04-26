using System.Collections.Generic;
using Experiment.Core.Domain;
using Experiment.Core.Entities;
using Experiment.Core.Enums;
using FluentAssertions;
using Xunit;

namespace Experiment.Tests.Experiment.Core.Tests.DiscountStrategies;

public class ForEvery100DollarsDiscountStrategyTests
{
    [Fact]
    public void ShouldNotApplyAnyDiscount_WhenTotalCostIsBelow100Dollars()
    {
        // ARRANGE
        var strategy = new ForEvery100DollarsDiscountStrategy();

        var user = new User();
        var invoice = new Invoice()
        {
            Products = new List<Product>()
            {
                new Product() { ProductType = ProductType.NonGrocery, Price = 50},
            }
        };

        // ACT
        var discount = strategy.Calculate(user, invoice);

        // ASSERT
        discount.Amount.Should().Be(0);
        discount.Title.Should().Be(nameof(ForEvery100DollarsDiscountStrategy));
    }

    [Theory]
    [InlineData(100, 5)]
    [InlineData(125, 5)]
    [InlineData(900, 45)]
    public void ShouldApplyDiscountOf5DollarsForEvery100Dollars(decimal productPrice, decimal expectedDiscount)
    {
        // ARRANGE
        var strategy = new ForEvery100DollarsDiscountStrategy();

        var user = new User();
        var invoice = new Invoice()
        {
            Products = new List<Product>()
            {
                new Product() { ProductType = ProductType.NonGrocery, Price = productPrice},
            }
        };

        // ACT
        var discount = strategy.Calculate(user, invoice);

        // ASSERT
        discount.Amount.Should().Be(expectedDiscount);
        discount.Title.Should().Be(nameof(ForEvery100DollarsDiscountStrategy));
    }
}