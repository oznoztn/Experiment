using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Experiment.API.Models.v1;

public class GetDiscountRequest
{
    [Required]
    [FromRoute(Name = "invoiceId")]

    public Guid InvoiceId { get; set; }
}