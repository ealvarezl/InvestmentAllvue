using System.ComponentModel.DataAnnotations;

namespace InvestmentTracker.Models;

public class InvestmentLot
{
    public int Id { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Shares must be greater than 0.")]
    public int Shares { get; set; }

    [Required]
    [Range(0.10, double.MaxValue, ErrorMessage = "Price per share must be greater than 0.")]
    public decimal PricePerShare { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime PurchaseDate { get; set; }
}
