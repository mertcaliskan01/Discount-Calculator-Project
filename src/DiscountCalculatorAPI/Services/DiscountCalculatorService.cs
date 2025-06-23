using DiscountCalculatorAPI.Models;

namespace DiscountCalculatorAPI.Services;

public class DiscountCalculatorService
{
    private readonly IEnumerable<IDiscountStrategy> _strategies;

    public DiscountCalculatorService(IEnumerable<IDiscountStrategy> strategies)
    {
        _strategies = strategies;
    }

    public DiscountResponse CalculateDiscount(DiscountRequest request)
    {
        if (request.OriginalPrice <= 0)
            throw new ArgumentException("Fiyat 0'dan büyük olmalıdır.");
        if (request.Quantity is < 1 or > 100)
            throw new ArgumentException("Adet 1-100 arasında olmalıdır.");

        var response = new DiscountResponse
        {
            ProductName = request.ProductName,
            OriginalPrice = request.OriginalPrice
        };

        foreach (var strategy in _strategies)
        {
            switch (strategy)
            {
                case CategoryDiscountStrategy:
                    response.CategoryDiscount = strategy.Calculate(request);
                    break;
                case CustomerDiscountStrategy:
                    response.CustomerDiscount = strategy.Calculate(request);
                    break;
                case QuantityDiscountStrategy:
                    response.QuantityDiscount = strategy.Calculate(request);
                    break;
            }
        }

        return response;
    }
}
