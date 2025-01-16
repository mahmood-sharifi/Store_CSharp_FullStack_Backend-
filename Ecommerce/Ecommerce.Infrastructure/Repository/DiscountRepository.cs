using Ecommerce.Domain.Common.Exceptions;
using Ecommerce.Domain.Filters;
using Ecommerce.Domain.Interfaces;
using Ecommerce.Domain.Models;
using Ecommerce.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Repository
{
    public class DiscountRepository(EcommerceContext context) : IDiscountRepo
    {
        private readonly EcommerceContext _context = context;

        public async Task<int> CountAsync(DiscountFilterOptions filteringOptions)
        {
            string query;
            object[] parameters;

            {
                query = "SELECT * FROM count_discounts()";
                parameters = [];
            }

            var count = await _context.Database
                .SqlQueryRaw<int>(query, parameters)
                .ToListAsync();

            return count.FirstOrDefault();
        }

        public async Task<bool> DeleteByIdAsync(int id)
        {
            var query = "SELECT * FROM delete_discount({0})";
            var result = await _context.Database
                .SqlQueryRaw<bool>(query, id)
                .ToListAsync();

            if (!result.FirstOrDefault())
            {
                throw new DiscountNotFoundException();
            }

            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Discount>> GetAllAsync(DiscountFilterOptions filteringOptions)
        {
            var page = filteringOptions.Page ?? 1;
            var perPage = filteringOptions.PerPage ?? 10;
            return await _context
                .GetDiscounts(page, perPage)
                    .OrderBy(p => p.Id)
                    .ToListAsync();
        }

        public async Task<Discount> GetByIdAsync(int id)
        {
            var result = await _context.GetDiscountById(id).FirstOrDefaultAsync();
            return result ?? throw new DiscountNotFoundException();
        }

        public async Task<Discount> PartialUpdateByIdAsync(Discount entity, int id)
        {
            return await _context.PatchDiscount(id, entity.Title, entity.Description, entity.Type, entity.Percent, entity.Code).FirstAsync();
        }

        public async Task<Discount> UpsertAsync(Discount entity, int? id)
        {
            var Result = await _context.UpsertDiscount(entity.Title, entity.Description, entity.Type, entity.Percent, entity.Code, id).FirstAsync();
            await _context.SaveChangesAsync();
            return await _context.Discounts.Where(p => p.Id == Result.Id).FirstOrDefaultAsync() ?? throw new DiscountNotFoundException();
        }
    }
}