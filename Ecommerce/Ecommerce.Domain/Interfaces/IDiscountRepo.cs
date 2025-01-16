using Ecommerce.Domain.Filters;
using Ecommerce.Domain.Models;

namespace Ecommerce.Domain.Interfaces
{
    public interface IDiscountRepo :
        IBaseRepo<Discount, DiscountFilterOptions>,
        IUpsert<Discount, DiscountFilterOptions>,
        IPartialUpdate<Discount, DiscountFilterOptions>
    { }
}