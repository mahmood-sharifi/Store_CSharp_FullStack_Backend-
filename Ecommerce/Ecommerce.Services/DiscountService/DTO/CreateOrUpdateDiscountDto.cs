using System.Data;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.Services.DiscountService.DTO
{
    public class CreateOrUpdateDiscountDto
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }
        public required int Percent { get; set; }
        public required string Code { get; set; }
    }

    public class CreateOrUpdateDiscountDtoValidator : AbstractValidator<CreateOrUpdateDiscountDto>
    {
        public CreateOrUpdateDiscountDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.Description).NotEmpty().MinimumLength(3).MaximumLength(500);
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Percent).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}