using Data.Entities;

namespace Data.Repository
{
    public interface ISubscriptionPlanRepository
    {
        List<SubscriptionPlan> GetAll();
    }
}
