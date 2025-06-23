using Xunit;
using DiscountCalculatorAPI.Models;
using DiscountCalculatorAPI.Services;
using DiscountCalculatorAPI.Enums;

namespace DiscountCalculatorAPI.Tests;

public class DiscountCalculatorServiceTests
{
    private readonly DiscountCalculatorService _service;

    public DiscountCalculatorServiceTests()
    {
        var strategies = new List<IDiscountStrategy>
        {
            new CategoryDiscountStrategy(),
            new CustomerDiscountStrategy(),
            new QuantityDiscountStrategy()
        };
        _service = new DiscountCalculatorService(strategies);
    }

    [Fact]
    public void CalculateDiscount_ShouldCalculateCorrectly_ForVipCustomerAndElectronics()
    {
        var request = new DiscountRequest
        {
            ProductName = "Laptop",
            Category = "Elektronik",
            OriginalPrice = 10000,
            Quantity = 1,
            CustomerType = CustomerType.VIP
        };

        var result = _service.CalculateDiscount(request);

        Assert.Equal(10000, result.OriginalPrice);
        Assert.Equal(1500, result.CategoryDiscount); // %15
        Assert.Equal(500, result.CustomerDiscount);  // %5
        Assert.Equal(0, result.QuantityDiscount);    // < 5
        Assert.Equal(2000, result.TotalDiscount);
        Assert.Equal(8000, result.FinalPrice);
    }

    [Fact]
    public void CalculateDiscount_ShouldApplyCategoryDiscountOnly_ForNonVipAndLowQuantity()
    {
        var request = new DiscountRequest
        {
            ProductName = "T-Shirt",
            Category = "Giyim",
            OriginalPrice = 2000,
            Quantity = 2,
            CustomerType = CustomerType.Standart
        };

        var result = _service.CalculateDiscount(request);

        Assert.Equal(2000, result.OriginalPrice);
        Assert.Equal(400, result.CategoryDiscount);  // %20
        Assert.Equal(0, result.CustomerDiscount);    // Non VIP
        Assert.Equal(0, result.QuantityDiscount);    // < 5
        Assert.Equal(400, result.TotalDiscount);
        Assert.Equal(1600, result.FinalPrice);
    }

    [Fact]
    public void CalculateDiscount_ShouldApplyQuantityDiscount_ForQuantity5OrMore()
    {
        var request = new DiscountRequest
        {
            ProductName = "Chair",
            Category = "Ev & Yaşam",
            OriginalPrice = 500,
            Quantity = 5,
            CustomerType = CustomerType.Standart
        };

        var result = _service.CalculateDiscount(request);

        Assert.Equal(500, result.OriginalPrice);
        Assert.Equal(50, result.CategoryDiscount);   // %10
        Assert.Equal(0, result.CustomerDiscount);    // Non VIP
        Assert.Equal(50, result.QuantityDiscount);   // %10 for quantity >=5
        Assert.Equal(100, result.TotalDiscount);
        Assert.Equal(400, result.FinalPrice);
    }

    [Fact]
    public void CalculateDiscount_ShouldApplyAllDiscounts_WhenAllCriteriaMet()
    {
        var request = new DiscountRequest
        {
            ProductName = "Smartphone",
            Category = "Elektronik",
            OriginalPrice = 8000,
            Quantity = 10,
            CustomerType = CustomerType.VIP
        };

        var result = _service.CalculateDiscount(request);

        Assert.Equal(8000, result.OriginalPrice);
        Assert.Equal(1200, result.CategoryDiscount);  // 15%
        Assert.Equal(400, result.CustomerDiscount);   // 5%
        Assert.Equal(800, result.QuantityDiscount);   // 10% quantity
        Assert.Equal(2400, result.TotalDiscount);
        Assert.Equal(5600, result.FinalPrice);
    }

    [Fact]
    public void CalculateDiscount_ShouldReturnZeroDiscounts_ForUnknownCategoryAndNonVipLowQuantity()
    {
        var request = new DiscountRequest
        {
            ProductName = "Book",
            Category = "Kitap",
            OriginalPrice = 100,
            Quantity = 1,
            CustomerType = CustomerType.Standart
        };

        var result = _service.CalculateDiscount(request);

        Assert.Equal(100, result.OriginalPrice);
        Assert.Equal(0, result.CategoryDiscount);    // Unknown category
        Assert.Equal(0, result.CustomerDiscount);    // Non VIP
        Assert.Equal(0, result.QuantityDiscount);    // <5 quantity
        Assert.Equal(0, result.TotalDiscount);
        Assert.Equal(100, result.FinalPrice);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-10)]
    public void CalculateDiscount_ShouldThrowArgumentException_WhenOriginalPriceIsZeroOrNegative(decimal price)
    {
        var request = new DiscountRequest
        {
            ProductName = "Invalid Product",
            Category = "Elektronik",
            OriginalPrice = price,
            Quantity = 1,
            CustomerType = CustomerType.VIP
        };

        var ex = Assert.Throws<ArgumentException>(() => _service.CalculateDiscount(request));
        Assert.Equal("Fiyat 0'dan büyük olmalıdır.", ex.Message);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(101)]
    public void CalculateDiscount_ShouldThrowArgumentException_WhenQuantityOutOfRange(int quantity)
    {
        var request = new DiscountRequest
        {
            ProductName = "Invalid Product",
            Category = "Elektronik",
            OriginalPrice = 1000,
            Quantity = quantity,
            CustomerType = CustomerType.VIP
        };

        var ex = Assert.Throws<ArgumentException>(() => _service.CalculateDiscount(request));
        Assert.Equal("Adet 1-100 arasında olmalıdır.", ex.Message);
    }

    
}
