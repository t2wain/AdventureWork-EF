using AdvWorkDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdvWorkConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Host.CreateDefaultBuilder()
                .Build()
                .Run();
        }

        public static IHost BuildHost()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    var cnnString = context.Configuration.GetConnectionString("Default")!;
                    SetupDBContext(services, cnnString, 2);
                    services.AddScoped<AdvWorkRepo>();
                })
                .Build();
        }

        public static void SetupDBContext(IServiceCollection services, string cnnString, int mode = 2)
        {
            #region Set options

            void SetOption(DbContextOptionsBuilder options)
            {
                options.UseSqlServer(cnnString);
                // read-only DBContext
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.EnableSensitiveDataLogging(false);
            };

            #endregion

            if (mode == 1)
                services.AddDbContextPool<AdvWorkDbContext>(SetOption);
            else if (mode == 2 || mode == 5)
                // Using DBContext pooling which is independent of
                // datbase connection pooling implemented by the client machine.
                // DBContext pooling feature does not allow a default constructor.
                services.AddPooledDbContextFactory<AdvWorkDbContext>(SetOption);
            else if (mode == 3)
                services.AddSingleton<IDbContextFactory<AdvWorkDbContext>, AdvWorksDbFact>();
            else if (mode == 4)
                services.AddDbContext<AdvWorkDbContext>(SetOption);
            else
                services.AddDbContextFactory<AdvWorkDbContext>(SetOption);
        }
    }
}