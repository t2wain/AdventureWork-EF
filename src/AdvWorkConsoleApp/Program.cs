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
                    services.AddDbContextFactory<AdvWorkDbContext>(options => 
                    {
                        options.UseSqlServer(context.Configuration.GetConnectionString("Default")!);
                        // read-only DBContext
                        options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                        options.EnableSensitiveDataLogging(false);
                    });

                    services.AddScoped<AdvWorkRepo>();
                })
                .Build();
        }
    }
}