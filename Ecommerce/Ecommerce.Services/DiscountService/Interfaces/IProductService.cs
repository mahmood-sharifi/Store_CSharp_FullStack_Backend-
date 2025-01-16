using Ecommerce.Domain.Filters;
using Ecommerce.Domain.Models;
using Ecommerce.Services.DiscountService.DTO;
using Ecommerce.Services.Common.Interfaces;

namespace Ecommerce.Services.DiscountService.Interfaces
{
    public interface IDiscountService : IBaseService<Discount, DiscountFilterOptions, GetDiscountDto>
    {
        Task<GetDiscountDto> UpsertAsync(CreateOrUpdateDiscountDto dto, int? id = null);
        Task<GetDiscountDto> PartialUpdateByIdAsync(PartialUpdateDiscountDto dto, int id);
    }
}