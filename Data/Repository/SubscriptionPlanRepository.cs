using Data.Entities;

namespace Data.Repository
{
    public class SubscriptionPlanRepository : ISubscriptionPlanRepository
    {
        private readonly ApplicationContext _context;
        public SubscriptionPlanRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<SubscriptionPlan> GetAll()
        {
            return _context.SubscriptionPlans.Where(sp => sp.Name != "Trial").ToList();
        }

        public SubscriptionPlan? GetByName(string name)
        {
            return _context.SubscriptionPlans.FirstOrDefault(p => p.Name == name);
        }
    }
}
