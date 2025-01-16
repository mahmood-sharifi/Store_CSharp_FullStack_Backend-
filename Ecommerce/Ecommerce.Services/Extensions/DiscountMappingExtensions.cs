using Ecommerce.Domain.Models;
using Ecommerce.Services.DiscountService.DTO;

namespace Ecommerce.Services.Extensions
{
    public static class DiscountMappingExtensions
    {
        public static Discount ToDiscount(this CreateOrUpdateDiscountDto dto, int? id)
        {
            return new Discount
            {
                Id = id ?? 0,
                Title = dto.Title,
                Description = dto.Description,
                Type = dto.Type,
                Percent = dto.Percent,
                Code = dto.Code,
            };
        }

        public static Discount ToDiscount(this PartialUpdateDiscountDto dto, Discount entity)
        {
            entity.Title = dto.Title ?? entity.Title;
            entity.Description = dto.Description ?? entity.Description;
            entity.Type = dto.Type ?? entity.Type;
            entity.Percent = dto.Percent ?? entity.Percent;
            entity.Code = dto.Code ?? entity.Code;

            return entity;
        }

        public static GetDiscountDto ToGetDiscountDto(this Discount entity)
        {
            return new GetDiscountDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                Type = entity.Type,
                Percent = entity.Percent,
                Code = entity.Code,
            };
        }
    }
}