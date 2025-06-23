using DiscountCalculatorAPI.Models;

namespace DiscountCalculatorAPI.Services;

public class QuantityDiscountStrategy : IDiscountStrategy
{
    public decimal Calculate(DiscountRequest request)
    {
        return request.Quantity >= 5 ? request.OriginalPrice * 0.10m : 0m;
    }
}
