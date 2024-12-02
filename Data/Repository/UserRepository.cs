using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users.Include(u => u.SubscriptionPlan).Include(u => u.ConversionHistory).Include(u => u.FavedCurrencies).ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users.Include(u => u.SubscriptionPlan).Include(u => u.ConversionHistory).ThenInclude(ch => ch.FromCurrency).Include(u => u.ConversionHistory).ThenInclude(ch => ch.ToCurrency).Include(u => u.FavedCurrencies).FirstOrDefault(u => u.Id == id && u.AccountStatus == true);
        }

        public User? GetByUsername(string username)
        {
            return _context.Users.Include(u => u.SubscriptionPlan).Include(u => u.ConversionHistory).Include(u => u.FavedCurrencies).FirstOrDefault(u => u.Username == username && u.AccountStatus == true);
        }

        public void AddUser(User newUser)
        {
            newUser.Id = _context.Users.Count() + 1;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
