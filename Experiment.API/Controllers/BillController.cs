using Experiment.API.Models.v1;
using Experiment.Service;
using Experiment.Service.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Experiment.API.Controllers
{
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillService _billService;

        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpGet]
        [Route("/bill/discount/{invoiceId:guid}")]
        public async Task<IActionResult> GetFinalAmountWithDiscountAsync([FromRoute]GetDiscountRequest request)
        {
            if (ModelState.IsValid)
            {
                InvoiceSummaryDto result = await _billService.GetInvoiceSummaryAsync(request.InvoiceId);

                if (result == null)
                    return NoContent();

                return Ok(result);
            }

            return BadRequest();
        }
    }
}