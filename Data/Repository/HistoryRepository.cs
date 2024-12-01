using Data.Entities;

namespace Data.Repository
{
    public class HistoryRepository : IHistoryRepository
    {
        private readonly ApplicationContext _context;
        public HistoryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public List<History> GetByUserId(int id)
        {
            return _context.Histories.Where(h => h.UserId == id).ToList();
        }
    }
}
