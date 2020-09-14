namespace Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        private AppDbContext _dbContext;

        public AppDbContext Init()
        {
            return _dbContext ?? (_dbContext = new AppDbContext());
        }

        protected override void DisposeCore()
        {
            if (_dbContext != null)
                _dbContext.Dispose();
        }
    }
}