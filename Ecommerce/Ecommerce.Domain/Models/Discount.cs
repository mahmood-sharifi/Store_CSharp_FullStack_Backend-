using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Models
{
    public class Discount : BaseEntity
    {
        [Column(TypeName = "varchar(100)")]
        public required string Title { get; set; }
        [Column(TypeName = "varchar(500)")]
        public required string Description { get; set; }
        [Column(TypeName = "varchar(100)")]
        public required string Type { get; set; }
        public required int Percent { get; set; }
        public required string Code { get; set; }
    }
}
