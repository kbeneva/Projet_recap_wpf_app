using IdeaManager.Data.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdeaManager.Data
{
    public class IdeaDbContextFactory : IDesignTimeDbContextFactory<IdeaDbContext>
    {
        public IdeaDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdeaDbContext>();
            optionsBuilder.UseSqlite("Data Source=ideas.db");

            return new IdeaDbContext(optionsBuilder.Options);
        }
    }
}
