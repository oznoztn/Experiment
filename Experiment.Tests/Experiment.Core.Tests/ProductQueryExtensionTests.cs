using System.Collections.Generic;
using Experiment.Core.Entities;
using Experiment.Core.Enums;
using Experiment.Core.QueryExtensions;
using FluentAssertions;
using Xunit;

namespace Experiment.Tests.Experiment.Core.Tests
{
    public class ProductQueryExtensionTests
    {
        [Fact]
        public void FilterNonGroceries_FiltersGroceryProducts()
        {
            // ARRANGE
            var prods = new List<Product>()
            {
                new Product()
                {
                    Price = 50,
                    ProductType = ProductType.Grocery
                },
                new Product()
                {
                    Price = 75,
                    ProductType = ProductType.NonGrocery
                }
            };

            // ASSERT
            var filteredProducst = prods.FilterNonGroceries();
            
            // ACT
            filteredProducst.Should().BeEquivalentTo(new List<Product>()
            {
                new Product()
                {
                    Price = 75,
                    ProductType = ProductType.NonGrocery
                }
            });
        }

        [Fact]
        public void FilterNonGroceries_ReturnsAnEmptyList_WhenInputListDoesntContainNonGroceryProducts()
        {
            // ARRANGE
            var prods = new List<Product>()
            {
                new Product()
                {
                    Price = 50,
                    ProductType = ProductType.Grocery
                },
                new Product()
                {
                    Price = 75,
                    ProductType = ProductType.Grocery
                }
            };

            // ASSERT
            var filteredProducst = prods.FilterNonGroceries();

            // ACT
            filteredProducst.Should().BeEquivalentTo(new List<Product>());
        }

        [Fact]
        public void SumNonGroceriesTotalCost_ReturnsTheSumOfNonGroceryProducts_WhenAListOfProductsIsGiven()
        {
            // ARRANGE
            var prods = new List<Product>()
            {
                new Product()
                {
                    Price = 50,
                    ProductType = ProductType.NonGrocery
                },
                new Product()
                {
                    Price = 100,
                    ProductType = ProductType.NonGrocery
                },
            };

            // ACT
            var totalCost = prods.SumNonGroceriesTotalCost();

            // ASSERT
            totalCost.Should().Be(150);
        }

        [Fact]
        public void SumNonGroceriesTotalCost_ReturnsZero_WhenInputProductsListDoesntContainAnyNonGroceryProduct()
        {
            // ARRANGE
            var prods = new List<Product>()
            {
                new Product()
                {
                    Price = 50,
                    ProductType = ProductType.Grocery
                },
            };

            // ACT
            var totalCost = prods.SumNonGroceriesTotalCost();

            // ASSERT
            totalCost.Should().Be(0);
        }
    }
}