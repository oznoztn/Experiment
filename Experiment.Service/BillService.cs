using Experiment.Core.Domain;
using Experiment.Core.Entities;
using Experiment.Core.Factory;
using Experiment.Core.Repository;
using Experiment.Service.DTOs;

namespace Experiment.Service;

public interface IBillService
{
    Task<InvoiceSummaryDto> GetInvoiceSummaryAsync(Guid invoideId);
}

public class BillService : IBillService
{
    private readonly IUserRepository _userRepository;
    private readonly IInvoiceRepository _invoiceResRepository;
    private readonly CalculationStrategyFactory _calculationStrategyFactory;

    public BillService(
        IUserRepository userRepository,
        IInvoiceRepository invoiceResRepository,
        CalculationStrategyFactory calculationStrategyFactory)
    {
        _userRepository = userRepository;
        _invoiceResRepository = invoiceResRepository;
        _calculationStrategyFactory = calculationStrategyFactory;
    }

    public async Task<InvoiceSummaryDto> GetInvoiceSummaryAsync(Guid invoideId)
    {
        var invoice = await _invoiceResRepository.GetInvoiceByIdAsync(invoideId);
        if (invoice == null)
            return null;

        var user = await _userRepository.GetUserByIdAsync(invoice.UserId);
        if (user== null)
            return null;

        IDiscountCalculationStrategy userDiscountStrat = _calculationStrategyFactory.Create(user, invoice);
        Discount userDiscount = userDiscountStrat.Calculate(user, invoice);

        IDiscountCalculationStrategy forEvery100DollarStrategy = _calculationStrategyFactory.CreateForEvery100DollarsDiscountStrategy(user, invoice);
        var discount2 = forEvery100DollarStrategy.Calculate(user, invoice);

        var appliedDiscounts = new List<Discount>();
        if (userDiscount.Amount != default)
            appliedDiscounts.Add(userDiscount);
        if(discount2.Amount != default)
            appliedDiscounts.Add(discount2);

        var totalCost = invoice.Products.Sum(t => t.Price);
        return new InvoiceSummaryDto()
        {
            UserId = user.Id,
            UserType = user.UserType.ToString(),
            Cost = totalCost,
            Discount = userDiscount.Amount + discount2.Amount,
            FinalCost = totalCost - userDiscount.Amount - discount2.Amount,
            AppliedDiscounts = appliedDiscounts
        };
    }
}