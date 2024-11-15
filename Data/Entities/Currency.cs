using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class Currency
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Symbol { get; set; }
        public required string ISOCode { get; set; }
        public required double ExchangeRate { get; set; }
        public bool Status { get; set; } = true;
    }
}
