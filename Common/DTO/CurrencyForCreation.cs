using System.ComponentModel.DataAnnotations;

namespace Common.DTO
{
    public class CurrencyForCreation
    {
        [Required]
        public required string Name { get; set; }
        [Required]
        public required string Symbol { get; set; }
        [Required]
        public required string ISOCode { get; set; }
        [Required]
        public required double ExchangeRate { get; set; }
    }
}
