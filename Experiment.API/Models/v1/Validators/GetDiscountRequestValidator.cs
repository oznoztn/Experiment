using FluentValidation;

namespace Experiment.API.Models.v1.Validators
{
    public class GetDiscountRequestValidator : AbstractValidator<GetDiscountRequest>
    {
        public GetDiscountRequestValidator()
        {
            RuleFor(t => t.InvoiceId).NotEmpty().WithMessage("Invoice field is required.");
        }
    }
}
