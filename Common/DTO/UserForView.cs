using Data.Entities;

namespace Common.DTO
{
    public class UserForView
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public bool IsAdmin { get; set; } = false;
        public int SubscriptionPlanId { get; set; }
        public ICollection<Currency> FavedCurrencies { get; set; }
        public ICollection<History> ConversionHistory { get; set; }
        public bool AccountStatus { get; set; }
    }
}
