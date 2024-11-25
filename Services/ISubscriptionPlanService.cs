using Common.DTO;

namespace Services
{
    public interface ISubscriptionPlanService
    {
        List<PlanForView> GetAllPlans();
    }
}
