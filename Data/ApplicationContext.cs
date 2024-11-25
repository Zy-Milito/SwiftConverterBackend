using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<History> Histories { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SubscriptionPlan free = new SubscriptionPlan()
            {
                Id = 1,
                Name = "Free",
                Description = "Our Free Plan is perfect for occasional users. Enjoy up to 10 conversions per month at no cost. Ideal for trying out our service and experiencing its core features.",
                Price = 0,
                MaxConversions = 10
            };
            SubscriptionPlan basic = new SubscriptionPlan()
            {
                Id = 2,
                Name = "Basic",
                Description = "Upgrade to the Basic Plan and unlock up to 100 conversions. This plan is great for regular users who need more flexibility and higher conversion limits.",
                Price = 1.91,
                MaxConversions = 100
            };
            SubscriptionPlan pro = new SubscriptionPlan()
            {
                Id = 3,
                Name = "Pro",
                Description = "For the power users. Experience the full potential of our service with unlimited conversions and no restrictions on your usage!",
                Price = 5.15,
                MaxConversions = null
            };
            SubscriptionPlan trial = new SubscriptionPlan()
            {
                Id = 4,
                Name = "Trial",
                Description = "",
                Price = 0,
                MaxConversions = 100
            };

            modelBuilder.Entity<SubscriptionPlan>().HasData(free, basic, pro, trial);

            User admin = new User()
            {
                Id = 1,
                Username = "admin",
                Password = "admin",
                Email = "admin@mail.com",
                IsAdmin = true,
                SubscriptionPlanId = 3
            };
            User zyther = new User()
            {
                Id = 2,
                Username = "Zyther",
                Password = "010101",
                Email = "zyther@mail.com",
                SubscriptionPlanId = 1
            };
            User caleb = new User()
            {
                Id = 3,
                Username = "Caleb",
                Password = "calebthepro",
                Email = "caleb@mail.com",
                SubscriptionPlanId = 2
            };
            User lumion = new User()
            {
                Id = 4,
                Username = "Lumion",
                Password = "master",
                Email = "lumion@mail.com",
                SubscriptionPlanId = 3
            };

            modelBuilder.Entity<User>().HasData(admin, zyther, caleb, lumion);

            Currency USD = new Currency()
            {
                Id = 1,
                Name = "United States Dollar",
                Symbol = "$",
                ISOCode = "USD",
                ExchangeRate = 1
            };
            Currency EUR = new Currency()
            {
                Id = 2,
                Name = "Euro",
                Symbol = "€",
                ISOCode = "EUR",
                ExchangeRate = 1.12
            };
            Currency JPY = new Currency()
            {
                Id = 3,
                Name = "Japanese Yen",
                Symbol = "¥",
                ISOCode = "JPY",
                ExchangeRate = 0.0091
            };
            Currency GBP = new Currency()
            {
                Id = 4,
                Name = "Pound Sterling",
                Symbol = "£",
                ISOCode = "GBP",
                ExchangeRate = 1.25
            };
            Currency CAD = new Currency()
            {
                Id = 5,
                Name = "Canadian Dollar",
                Symbol = "$",
                ISOCode = "CAD",
                ExchangeRate = 0.79
            };
            Currency AUD = new Currency()
            {
                Id = 6,
                Name = "Australian Dollar",
                Symbol = "$",
                ISOCode = "AUD",
                ExchangeRate = 0.72
            };
            Currency INR = new Currency()
            {
                Id = 7,
                Name = "Indian Rupee",
                Symbol = "₹",
                ISOCode = "INR",
                ExchangeRate = 0.013
            };
            Currency ARS = new Currency()
            {
                Id = 8,
                Name = "Argentine Peso",
                Symbol = "$",
                ISOCode = "ARS",
                ExchangeRate = 0.0010
            };
            Currency KRW = new Currency()
            {
                Id = 9,
                Name = "South Korean Won",
                Symbol = "₩",
                ISOCode = "KRW",
                ExchangeRate = 0.00084
            };
            Currency CNY = new Currency()
            {
                Id = 10,
                Name = "Chinese Yuan",
                Symbol = "¥",
                ISOCode = "CNY",
                ExchangeRate = 0.15
            };

            modelBuilder.Entity<Currency>().HasData(USD, EUR, JPY, GBP, CAD, AUD, INR, ARS, KRW, CNY);

            modelBuilder.Entity<User>().HasOne(u => u.SubscriptionPlan).WithMany(sp => sp.Users).HasForeignKey(u => u.SubscriptionPlanId);
            modelBuilder.Entity<User>().HasMany(u => u.FavedCurrencies).WithMany();
            modelBuilder.Entity<User>().HasMany(u => u.ConversionHistory).WithOne(ch => ch.User).HasForeignKey(ch => ch.UserId);

            modelBuilder.Entity<History>().HasOne(h => h.FromCurrency).WithMany().HasForeignKey(h => h.FromCurrencyId);
            modelBuilder.Entity<History>().HasOne(h => h.ToCurrency).WithMany().HasForeignKey(h => h.ToCurrencyId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
