using System.ComponentModel.DataAnnotations;
using DiscountCalculatorAPI.Enums;

namespace DiscountCalculatorAPI.Models;

public class DiscountRequest
{
    [Required]
    public required string ProductName { get; set; }

    [Required]
    public required string Category { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public required decimal OriginalPrice { get; set; }

    [Required]
    [Range(1, 100, ErrorMessage = "Quantity must be between 1 and 100")]
    public required int Quantity { get; set; }

    [Required]
    public required CustomerType CustomerType { get; set; }

}
