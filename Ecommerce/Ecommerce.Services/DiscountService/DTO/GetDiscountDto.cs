using Ecommerce.Domain.Models;
using Ecommerce.Services.Common.DTO;

namespace Ecommerce.Services.DiscountService.DTO
{
    public class GetProducImageDto
    {
        public required string Url { get; set; }
    }
    public class GetDiscountDto : IReadDto<Discount>
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public required string Type { get; set; }
        public required int Percent { get; set; }
        public required string Code { get; set; }



        public void FromEntity(Discount entity)
        {
            Id = entity.Id;
            Title = entity.Title;
            Description = entity.Description;
            Type = entity.Type;
            Percent = entity.Percent;
            Code = entity.Code;
        }
    }
}