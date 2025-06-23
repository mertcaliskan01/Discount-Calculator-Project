namespace DiscountCalculatorAPI.Models;

public class DiscountResponse
{
    public string ProductName { get; set; } = string.Empty;
    public decimal OriginalPrice { get; set; }
    public decimal CategoryDiscount { get; set; } = 0;
    public decimal CustomerDiscount { get; set; } = 0;
    public decimal QuantityDiscount { get; set; } = 0;
    public decimal TotalDiscount => CategoryDiscount + CustomerDiscount + QuantityDiscount;
    public decimal FinalPrice => OriginalPrice - TotalDiscount;
}
