using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AdvWorkDB
{
    public class AdvWorksDbFact : IDbContextFactory<AdvWorkDbContext>
    {
        string _cnnString;

        public AdvWorksDbFact(IConfiguration config)
        {
            this._cnnString = config.GetConnectionString("Default")!;
        }

        public AdvWorkDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AdvWorkDbContext>()
                .UseSqlServer(_cnnString)
                // read-only DBContext
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging(false);
            return new AdvWorkDbContext(options.Options);
        }

        public AdvWorkDbContext CreateDbContext(string cnnString)
        {
            var options = new DbContextOptionsBuilder<AdvWorkDbContext>()
                .UseSqlServer(cnnString)
                // read-only DBContext
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .EnableSensitiveDataLogging(false);
            return new AdvWorkDbContext(options.Options);
        }
    }
}
