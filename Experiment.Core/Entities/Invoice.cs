namespace Experiment.Core.Entities;

public class Invoice
{
    /// <summary>
    /// Invoice Id
    /// </summary>
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<Product> Products { get; set; }
}