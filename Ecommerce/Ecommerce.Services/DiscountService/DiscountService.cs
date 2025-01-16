using Ecommerce.Domain.Filters;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Services.DiscountService.DTO;
using Ecommerce.Services.DiscountService.Interfaces;
using Ecommerce.Services.Common;
using Ecommerce.Services.Extensions;

namespace Ecommerce.Services.DiscountService
{
    public class DiscountService(IDiscountRepo DiscountRepo) : BaseService<Discount, DiscountFilterOptions, GetDiscountDto>(DiscountRepo), IDiscountService
    {
        private readonly IDiscountRepo _repo = DiscountRepo;

        public async Task<GetDiscountDto> PartialUpdateByIdAsync(PartialUpdateDiscountDto dto, int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            var updatedEntity = dto.ToDiscount(entity);
            var result = await _repo.PartialUpdateByIdAsync(updatedEntity, id);
            return result.ToGetDiscountDto();
        }

        public async Task<GetDiscountDto> UpsertAsync(CreateOrUpdateDiscountDto dto, int? id = null)
        {
            var result = await _repo.UpsertAsync(dto.ToDiscount(id), id);
            return result.ToGetDiscountDto();
        }
    }
}