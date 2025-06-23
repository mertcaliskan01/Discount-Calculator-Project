using DiscountCalculatorAPI.Enums;
using DiscountCalculatorAPI.Models;

namespace DiscountCalculatorAPI.Services;

public class CustomerDiscountStrategy : IDiscountStrategy
{
    public decimal Calculate(DiscountRequest request)
    {
        return request.CustomerType == CustomerType.VIP ? request.OriginalPrice * 0.05m : 0m;
    }
}
