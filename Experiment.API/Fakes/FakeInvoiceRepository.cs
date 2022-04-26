using System.Diagnostics.CodeAnalysis;
using Experiment.Core.Entities;
using Experiment.Core.Enums;
using Experiment.Core.Repository;

namespace Experiment.API.Fakes
{
    // No need to unit test fake implementations
    [ExcludeFromCodeCoverage]
    public class FakeInvoiceRepository : IInvoiceRepository
    {
        private static readonly List<Invoice> Invoices = new()
        {
            new Invoice()
            {
                Id = Guid.Parse("0246a66d-0627-4f11-aadd-2f91a725cf76"),
                UserId = Guid.Parse("0246a66d-0627-4f11-aadd-2f91a725cf76"),
                Products = new List<Product>()
                {
                    new(){ Price = 100, ProductType = ProductType.NonGrocery },
                    new(){ Price = 200, ProductType = ProductType.NonGrocery },
                    new(){ Price = 200, ProductType = ProductType.NonGrocery },
                },
            },
            new Invoice()
            {
                Id = Guid.Parse("6b0b2241-cca8-478e-9f3c-c70c4444d46a"),
                UserId = Guid.Parse("6b0b2241-cca8-478e-9f3c-c70c4444d46a"),
                Products = new List<Product>()
                {
                    new(){ Price = 100, ProductType = ProductType.Grocery },
                    new(){ Price = 200, ProductType = ProductType.Grocery },
                    new(){ Price = 300, ProductType = ProductType.Grocery },
                    new(){ Price = 400, ProductType = ProductType.Grocery },
                    new(){ Price = 500, ProductType = ProductType.Grocery },
                },
            },
            new Invoice()
            {
                Id = Guid.Parse("0a331800-25e1-49f4-914a-11869a383ead"),
                UserId = Guid.Parse("0a331800-25e1-49f4-914a-11869a383ead"),
                Products = new List<Product>()
                {
                    new(){ Price = 700, ProductType = ProductType.NonGrocery },
                    new(){ Price = 1400, ProductType = ProductType.NonGrocery },
                    new(){ Price = 800, ProductType = ProductType.NonGrocery },
                },
            },
            new Invoice()
            {
                Id = Guid.Parse("14a78e3c-db40-49dc-b3ce-777982c5145f"),
                UserId = Guid.Parse("14a78e3c-db40-49dc-b3ce-777982c5145f"),
                Products = new List<Product>()
                {
                    new(){ Price = 400, ProductType = ProductType.NonGrocery },
                    new(){ Price = 200, ProductType = ProductType.NonGrocery },
                    new(){ Price = 500, ProductType = ProductType.NonGrocery },
                    new(){ Price = 900, ProductType = ProductType.NonGrocery }
                },
            },
            new Invoice()
            {
                Id = Guid.Parse("473cb273-5142-4ccd-9e40-8e6bb1eaf75b"),
                UserId = Guid.Parse("473cb273-5142-4ccd-9e40-8e6bb1eaf75b"),
                Products = new List<Product>()
                {
                    new(){ Price = 100, ProductType = ProductType.NonGrocery },
                    new(){ Price = 200, ProductType = ProductType.NonGrocery },
                },
            }
        };

        public async Task<Invoice> GetInvoiceByIdAsync(Guid invoiceId)
        {
            Invoice invoice = Invoices.FirstOrDefault(t => t.Id == invoiceId);

            return await Task.FromResult(invoice);
        }
    }
}
