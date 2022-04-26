using System;
using Experiment.Core.Domain;
using Experiment.Core.Entities;
using Experiment.Core.Enums;
using Experiment.Core.Factory;
using FluentAssertions;
using Xunit;

namespace Experiment.Tests.Experiment.Core.Tests.Factory
{
    public class CalculationStrategyFactoryTests
    {
        [Fact]
        public void CreateForEvery100DollarsDiscountStrategy_ThrowsArgumentNullException_WhenUserIsNull()
        {
            CalculationStrategyFactory factory = new CalculationStrategyFactory();

            var exception = Assert.Throws<ArgumentNullException>(() => factory.CreateForEvery100DollarsDiscountStrategy(user: null, invoice: new Invoice()));

            exception.ParamName.Should().Be("user");
        }

        [Fact]
        public void CreateForEvery100DollarsDiscountStrategy_ThrowsArgumentNullException_WhenInvoiceIsNull()
        {
            CalculationStrategyFactory factory = new CalculationStrategyFactory();

            var exception = Assert.Throws<ArgumentNullException>(() => factory.CreateForEvery100DollarsDiscountStrategy(user: new User(), invoice: null));

            exception.ParamName.Should().Be("invoice");
        }

        [Fact]
        public void CreateForEvery100DollarsDiscountStrategy_ReturnsAnInstanceOfForEvery100DollarsDiscountStrategy()
        {
            CalculationStrategyFactory factory = new CalculationStrategyFactory();

            IDiscountCalculationStrategy strategy = factory.CreateForEvery100DollarsDiscountStrategy(new User(), new Invoice());

            strategy.Should().BeOfType<ForEvery100DollarsDiscountStrategy>();
        }

        /*=============================================================*/

        [Fact]
        public void Create_ThrowsArgumentNullException_WhenUserIsNull()
        {
            CalculationStrategyFactory factory = new CalculationStrategyFactory();

            var exception = Assert.Throws<ArgumentNullException>(() => factory.Create(user: null, invoice: new Invoice()));

            exception.ParamName.Should().Be("user");
        }

        [Fact]
        public void Create_ThrowsArgumentNullException_WhenInvoiceIsNull()
        {
            CalculationStrategyFactory factory = new CalculationStrategyFactory();

            var exception = Assert.Throws<ArgumentNullException>(() => factory.Create(user: new User(), invoice: null));

            exception.ParamName.Should().Be("invoice");
        }

        [Fact]
        public void Create_ReturnsAffiliateDiscountStrategy_WhenGivenUserTypeIsAffiliate()
        {
            CalculationStrategyFactory factory = new CalculationStrategyFactory();

            IDiscountCalculationStrategy strategy = factory.Create(new User()
            {
                UserType = UserType.Affiliate
            }, new Invoice());

            strategy.Should().BeOfType<AffiliateDiscountStrategy>();
        }

        [Fact]
        public void Create_ReturnsCustomerDiscountStrategy_WhenGivenUserTypeIsCustomer()
        {
            CalculationStrategyFactory factory = new CalculationStrategyFactory();

            IDiscountCalculationStrategy strategy = factory.Create(new User()
            {
                UserType = UserType.Customer
            }, new Invoice());

            strategy.Should().BeOfType<CustomerDiscountStrategy>();
        }

        [Fact]
        public void Create_ReturnsEmployeeDiscountStrategy_WhenGivenUserTypeIsEmployee()
        {
            CalculationStrategyFactory factory = new CalculationStrategyFactory();

            IDiscountCalculationStrategy strategy = factory.Create(new User()
            {
                UserType = UserType.Employee
            }, new Invoice());


            strategy.Should().BeOfType<EmployeeDiscountStrategy>();
        }

        [Fact]
        public void Create_ReturnsNullDiscountStrategy_WhenGivenUserTypeIsUndefined()
        {
            CalculationStrategyFactory factory = new CalculationStrategyFactory();

            IDiscountCalculationStrategy strategy = factory.Create(new User()
            {
                UserType = UserType.Undefined
            }, new Invoice());


            strategy.Should().BeOfType<NullDiscountStrategy>();
        }
    }
}
