using Ecommerce.Domain.Models;
using Ecommerce.Services.Common.DTO;
using Ecommerce.Services.ProductService.DTO;

namespace Ecommerce.Services.CartItemService.DTO
{
    public class GetCartItemDto : IReadDto<CartItem>
    {
        public int Id { get; set; }
        public required int Quantity { get; set; }
        public required int UserId { get; set; }
        public required int ProductId { get; set; }
        public required GetProductDto Product { get; set; }
        public void FromEntity(CartItem entity)
        {
            Id = entity.Id;
            Quantity = entity.Quantity;
            UserId = entity.UserId;
            ProductId = entity.ProductId;

            Product = new GetProductDto
            {
                Id = entity.Product.Id,
                Title = entity.Product.Title,
                Description = entity.Product.Description,
                Price = entity.Product.Price,
                Stock = entity.Product.Stock,
                CategoryId = entity.Product.CategoryId,
                ProductImage = entity.Product.ProductImages?.Select(pi => new GetProducImageDto
                {
                    Url = pi.Url
                }).ToList() ?? new List<GetProducImageDto>()
            };
        }
    }
}