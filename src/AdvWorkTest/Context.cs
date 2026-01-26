using AdvWorkConsoleApp;
using AdvWorkDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AdvWorkTest
{
    public class Context : IDisposable
    {
        IHost _host;
        IDbContextFactory<AdvWorkDbContext> _dbfact;

        public Context()
        {
            _host = Program.BuildHost();
            _dbfact = _host.Services.GetRequiredService<IDbContextFactory<AdvWorkDbContext>>();
        }

        public AdvWorkDbContext NewDB() => _dbfact.CreateDbContext();

        public void Dispose()
        {
            _host.Dispose();
            _host = null!;
        }
    }
}
