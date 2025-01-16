using System.ComponentModel.DataAnnotations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;

namespace Ecommerce.Services.DiscountService.DTO
{
    public class PartialUpdateDiscountDto()
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public int? Percent { get; set; }
        public string? Code { get; set; }
    }

    public class PartialUpdateDiscountDtoValidator : AbstractValidator<PartialUpdateDiscountDto>
    {
        public PartialUpdateDiscountDtoValidator()
        {
            RuleFor(x => x.Title).MinimumLength(3).MaximumLength(50);
            RuleFor(x => x.Description).MinimumLength(3).MaximumLength(500);
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Percent).NotEmpty().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}