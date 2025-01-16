namespace Ecommerce.Domain.Common.Exceptions
{
    public class DiscountNotFoundException(string message = "Discount not found") : Exception(message)
    { }
}
