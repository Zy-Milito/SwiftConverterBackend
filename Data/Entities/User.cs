using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public bool IsAdmin { get; set; } = false;
        public int SubscriptionPlanId { get; set; } = 1;
        public SubscriptionPlan SubscriptionPlan { get; set; }
        public ICollection<Currency> FavedCurrencies { get; set; }
        public ICollection<History> ConversionHistory { get; set; }
        public bool AccountStatus { get; set; } = true;

        public User()
        {
            this.FavedCurrencies = new List<Currency>();
            this.ConversionHistory = new List<History>();
        }
    }
}
