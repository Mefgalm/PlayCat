namespace PlayCat.DataService
{
    public class BaseService
    {
        protected PlayCatDbContext _dbContext;

        public BaseService(PlayCatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
