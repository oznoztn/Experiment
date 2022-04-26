using System.Collections.Generic;
using Experiment.Core.Domain;
using Experiment.Core.Entities;
using Experiment.Core.Enums;
using FluentAssertions;
using Xunit;

namespace Experiment.Tests.Experiment.Core.Tests.DiscountStrategies
{
    public class AffiliateDiscountStrategyTests
    {
        [Fact]
        public void ShouldApplyDiscountOf10Percent_IfTheUserIsAnAffiliate()
        {
            // ARRANGE
            var user = new User()
            {
                UserType = UserType.Affiliate
            };

            Invoice invoice = new()
            {
                Products = new List<Product>()
                {
                    new Product() { ProductType = ProductType.NonGrocery, Price = 100},
                }
            };

            // ACT
            var strategy = new AffiliateDiscountStrategy();
            var discount = strategy.Calculate(user, invoice);

            // ASSERT
            discount.Amount.Should().Be(10);
            discount.Title.Should().Be(nameof(AffiliateDiscountStrategy));
        }

        [Fact]
        public void ShouldNotApplyDiscountOf10Percent_IfTheUserIsNotAnAffiliate()
        {
            // ARRANGE
            var user = new User()
            {
                UserType = UserType.Customer
            };

            Invoice invoice = new()
            {
                Products = new List<Product>()
                {
                    new Product() { ProductType = ProductType.NonGrocery, Price = 100},
                }
            };

            // ACT
            var strategy = new AffiliateDiscountStrategy();
            var discount = strategy.Calculate(user, invoice);

            // ASSERT
            discount.Amount.Should().Be(0);
            discount.Title.Should().Be(nameof(AffiliateDiscountStrategy));
        }
    }
}
