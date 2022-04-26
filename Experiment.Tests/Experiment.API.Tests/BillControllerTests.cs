using System;
using System.Threading.Tasks;
using Experiment.API.Controllers;
using Experiment.API.Models.v1;
using Experiment.Service;
using Experiment.Service.DTOs;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Experiment.Tests.Experiment.API.Tests;

public class BillControllerTests
{
    [Fact]
    public async Task GetFinalAmountWithDiscountAsync_ReturnsBadRequestResult_WhenModelValidationFails()
    {
        // ARRANGE
        var controller = new BillController(billService: null);
        controller.ModelState.AddModelError("", "");

        // ACT
        var result = await controller.GetFinalAmountWithDiscountAsync(new GetDiscountRequest());

        // ASSERT
        Assert.IsType<BadRequestResult>(result);
    }

    [Fact]
    public async Task GetFinalAmountWithDiscountAsync_ReturnsOkResultWithDiscount()
    {
        // ARRANGE
        Guid invoiceId = Guid.NewGuid();
        var mockBillService = new Mock<IBillService>();
        mockBillService
            .Setup(t => t.GetInvoiceSummaryAsync(invoiceId))
            .Returns(Task.FromResult(new InvoiceSummaryDto()
            {
                Discount = 100m
            }))
            .Verifiable();

        var controller = new BillController(mockBillService.Object);

        // ACT
        var result = await controller.GetFinalAmountWithDiscountAsync(new GetDiscountRequest()
        {
            InvoiceId = invoiceId
        });

        // ASSERT
        mockBillService.Verify();

        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var model = Assert.IsType<InvoiceSummaryDto>(okObjectResult.Value);
        
        Assert.Equal(100m, model.Discount);
    }

    public async Task GetFinalAmountWithDiscountAsync_ReturnsNotFoundResultWhenAnInvoiceWithRequestedIdDoesntExist()
    {
        // ARRANGE
        Guid invoiceId = Guid.NewGuid();
        var mockBillService = new Mock<IBillService>();
        mockBillService
            .Setup(t => t.GetInvoiceSummaryAsync(invoiceId))
            .Returns(Task.FromResult(default(InvoiceSummaryDto)))
            .Verifiable();

        var controller = new BillController(mockBillService.Object);

        // ACT
        var result = await controller.GetFinalAmountWithDiscountAsync(new GetDiscountRequest());

        // ASSERT
        mockBillService.Verify();

        Assert.IsType<NoContentResult>(result);
    }
}