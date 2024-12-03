using Common.DTO;
using Data.Repository;

namespace Services
{
    public class SubscriptionPlanService : ISubscriptionPlanService
    {
        private readonly ISubscriptionPlanRepository _subscriptionPlanRepository;
        private readonly IUserRepository _userRepository;
        public SubscriptionPlanService(ISubscriptionPlanRepository subscriptionPlanRepository, IUserRepository userRepository)
        {
            _subscriptionPlanRepository = subscriptionPlanRepository;
            _userRepository = userRepository;
        }

        public List<PlanForView> GetAllPlans()
        {
            var plans = _subscriptionPlanRepository.GetAll();

            return plans.Select(p => new PlanForView()
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                MaxConversions = p.MaxConversions,
            }).ToList();
        }

        public PlanForView? GetCurrentPlan(int id)
        {
            var plans = _subscriptionPlanRepository.GetAll();
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                throw new Exception("User not found.");
            }

            var plan = new PlanForView
            {
                Id = user.SubscriptionPlan.Id,
                Name = user.SubscriptionPlan.Name,
                Description = user.SubscriptionPlan.Description,
                Price = user.SubscriptionPlan.Price,
                MaxConversions = user.SubscriptionPlan.MaxConversions,
            };

            return plan;
        }
    }
}
