using Task = System.Threading.Tasks.Task;

namespace Data
{
    public class DbInitializer
    {
        private readonly AppDbContext _context;

        public DbInitializer(AppDbContext context)
        {
            _context = context;
        }

        public async Task Seed()
        {
            //todo
            await _context.SaveChangesAsync();
        }
    }
}