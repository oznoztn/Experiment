using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Experiment.Core.Domain;
using Experiment.Core.Entities;
using Experiment.Core.Enums;
using Experiment.Core.Factory;
using Experiment.Core.Repository;
using Experiment.Service;
using Experiment.Service.DTOs;
using FluentAssertions;
using Moq;
using Xunit;

namespace Experiment.Tests.Experiment.Service
{
    public class BillServiceTests
    {
        [Fact]
        public async Task GetInvoiceSummaryAsync_ReturnsNull_WhenInvoiceWithRequestedIdDoesntExist()
        {
            // ARRANGE
            var mockInvoiceRepository = new Mock<IInvoiceRepository>();
            mockInvoiceRepository
                .Setup(_ => _.GetInvoiceByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(default(Invoice))!)
                .Verifiable();

            var billService =
                new BillService(
                    userRepository: null, 
                    mockInvoiceRepository.Object,
                    new CalculationStrategyFactory());

            // ACT
            InvoiceSummaryDto result = await billService.GetInvoiceSummaryAsync(Guid.NewGuid());

            // ASSERT
            result.Should().BeNull();
        }
        
        [Fact]
        public async Task GetInvoiceSummaryAsync_ReturnsNull_WhenUserWithRequestedIdDoesntExist()
        {
            // ARRANGE
            var mockInvoiceRepository = new Mock<IInvoiceRepository>();
            mockInvoiceRepository
                .Setup(_ => _.GetInvoiceByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new Invoice()
                {
                    Products = new List<Product>()
                    {
                        new Product() {ProductType = ProductType.NonGrocery, Price = 100},
                    }
                }))
                .Verifiable();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(_ => _.GetUserByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(default(User)))
                .Verifiable();

            var billService =
                new BillService(
                    userRepository: mockUserRepository.Object, 
                    invoiceResRepository: mockInvoiceRepository.Object,
                    new CalculationStrategyFactory());

            // ACT
            InvoiceSummaryDto result = await billService.GetInvoiceSummaryAsync(Guid.NewGuid());

            // ASSERT
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetInvoiceSummaryAsync()
        {
            // ARRANGE
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(_ => _.GetUserByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new User()
                {
                    RegisterationDate = DateTime.UtcNow,
                    UserType = UserType.Affiliate
                }))
                .Verifiable();

            var mockInvoiceRepository = new Mock<IInvoiceRepository>();
            mockInvoiceRepository
                .Setup(_ => _.GetInvoiceByIdAsync(It.IsAny<Guid>()))
                .Returns(Task.FromResult(new Invoice()
                {
                    Products = new List<Product>()
                    {
                        new Product() {ProductType = ProductType.NonGrocery, Price = 100},
                    }
                }))
                .Verifiable();

            var billService =
                new BillService(mockUserRepository.Object, mockInvoiceRepository.Object,
                    new CalculationStrategyFactory());

            // ACT
            var result = await billService.GetInvoiceSummaryAsync(It.IsAny<Guid>());

            // ASSERT
            mockInvoiceRepository.Verify();
            mockInvoiceRepository.Verify();

            result.Discount.Should().Be(10 + 5);

            result.AppliedDiscounts.Should().BeEquivalentTo(new List<Discount>()
            {
                new Discount() { Amount = 10.00M, Title = nameof(AffiliateDiscountStrategy) },
                new Discount() { Amount = 5, Title = nameof(ForEvery100DollarsDiscountStrategy)}
            });
        }
    }
}