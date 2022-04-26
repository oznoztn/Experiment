using System;
using System.Collections.Generic;
using Experiment.Core.Domain;
using Experiment.Core.Entities;
using Experiment.Core.Enums;
using Experiment.Core.Infrastructure;
using FluentAssertions;
using Moq;
using Xunit;

namespace Experiment.Tests.Experiment.Core.Tests.DiscountStrategies;

public class CustomerDiscountStrategyTests
{
    [Fact]
    public void Calculate_ShouldApplyDiscountOfFivePercent_IfUserHasBeenCustomerMoreThanTwoYears()
    {
        // ARRANGE
        User user = new()
        {
            RegisterationDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            UserType = UserType.Customer
        };

        Invoice invoice = new()
        {
            Products = new List<Product>()
            {
                new Product() { ProductType = ProductType.NonGrocery, Price = 100},
            }
        };

        var mockClock = new Mock<IClock>();
        mockClock.Setup(t => t.GetCurrentDateTimeUtc()).Returns(new DateTime(2005, 1, 1, 0, 0, 0, DateTimeKind.Utc));

        CustomerDiscountStrategy strategy = new(mockClock.Object);

        // ACT
        var discount = strategy.Calculate(user, invoice);

        discount.Amount.Should().Be(5);
    }

    [Fact]
    public void Calculate_ShouldNotApplyDiscountOfFivePercent_IfUserHasBeenCustomerLessThanTwoYears()
    {
        // ARRANGE
        User user = new()
        {
            RegisterationDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc),
            UserType = UserType.Customer
        };

        Invoice invoice = new()
        {
            Products = new List<Product>()
            {
                new Product() { ProductType = ProductType.NonGrocery, Price = 100}
            }
        };

        var mockClock = new Mock<IClock>();
        mockClock.Setup(t => t.GetCurrentDateTimeUtc()).Returns(new DateTime(2000, 3, 3, 0, 0, 0, DateTimeKind.Utc));

        CustomerDiscountStrategy strategy = new(mockClock.Object);

        // ACT
        var discount = strategy.Calculate(user, invoice);

        discount.Amount.Should().Be(0);
    }

    [Fact]
    public void Calculate_ShouldNotApplyDiscountOfFivePercent_IfUserIsNotACustomer()
    {
        // ARRANGE
        CustomerDiscountStrategy strategy = new(new SystemClock());

        // ACT
        var discount = strategy.Calculate(user: new User()
        {
            UserType = UserType.Affiliate
        }, userInvoice: default);

        discount.Amount.Should().Be(0);
    }
}