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
    }
}
