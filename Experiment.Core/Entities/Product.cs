using Experiment.Core.Enums;

namespace Experiment.Core.Entities;

public class Product
{
    public Guid Id { get; set; }
    public ProductType ProductType { get; set; }
    public decimal Price { get; set; }
}