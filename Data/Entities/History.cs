using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required DateTime Date { get; set; } = DateTime.Now;
        public int UserId { get; set; }
        public required User User { get; set; } = null!;
        public required double Amount { get; set; }
        public int FromCurrencyId { get; set; }
        public required Currency FromCurrency { get; set; }
        public int ToCurrencyId { get; set; }
        public required Currency ToCurrency { get; set; }
    }
}
