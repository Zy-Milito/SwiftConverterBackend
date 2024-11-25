using Common.DTO;
using Data.Repository;

namespace Services
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        public SubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlanRepository)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
        }

        public List<PlanForView> GetAllPlans()
        {
            var plans = _subscriptionPlanRepository.GetAll();

            return plans.Select(p => new PlanForView()
            {
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                MaxConversions = p.MaxConversions,
            }).ToList();
        }
    }
}
