using Ecommerce.Domain.Models;
using Ecommerce.Services.CartItemService.DTO;
using Ecommerce.Services.ProductService.DTO;

namespace Ecommerce.Services.Extensions
{
    public static class CartItemMappingExtensions
    {
        public static CartItem ToCartItem(this CreateCartItemDto dto)
        {
            return new CartItem
            {
                Id = 0,
                ProductId = dto.ProductId,
                Quantity = dto.Quantity,
                UserId = dto.UserId,
                User = null!,
                Product = null!
            };
        }

        public static CartItem ToCartItem(this PartialUpdateCartItemDto dto, CartItem entity)
        {
            entity.Quantity = dto.Quantity;

            return entity;
        }

        public static GetCartItemDto ToGetCartItemDto(this CartItem entity)
        {
            return new GetCartItemDto
            {
                Id = entity.Id,
                ProductId = entity.ProductId,
                Quantity = entity.Quantity,
                UserId = entity.UserId,
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
                }
            };
        }
    }
}