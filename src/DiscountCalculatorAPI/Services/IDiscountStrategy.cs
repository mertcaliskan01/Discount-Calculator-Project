using DiscountCalculatorAPI.Models;

namespace DiscountCalculatorAPI.Services;

public interface IDiscountStrategy
{
    decimal Calculate(DiscountRequest request);
}
