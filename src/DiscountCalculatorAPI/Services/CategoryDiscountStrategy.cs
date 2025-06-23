using DiscountCalculatorAPI.Models;

namespace DiscountCalculatorAPI.Services;

public class CategoryDiscountStrategy : IDiscountStrategy
{
    public decimal Calculate(DiscountRequest request)
    {
        return request.Category switch
        {
            "Elektronik" => request.OriginalPrice * 0.15m,
            "Giyim" => request.OriginalPrice * 0.20m,
            "Ev & Yaşam" => request.OriginalPrice * 0.10m,
            _ => 0m
        };
    }
}
