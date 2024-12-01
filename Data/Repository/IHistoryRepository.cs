using Data.Entities;

namespace Data.Repository
{
    public interface IHistoryRepository
    {
        List<History> GetByUserId(int id);
    }
}
