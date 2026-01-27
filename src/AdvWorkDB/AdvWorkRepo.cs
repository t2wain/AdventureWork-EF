using AdvWorkEntity.Entity;
using Microsoft.EntityFrameworkCore;

namespace AdvWorkDB
{
    public class AdvWorkRepo
    {
        IDbContextFactory<AdvWorkDbContext> _dbfact;

        public AdvWorkRepo(IDbContextFactory<AdvWorkDbContext> dbfact)
        {
            this._dbfact = dbfact;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            using var db = NewDB();
            return db.Employees.ToList();
        }

        public IEnumerable<Person> GetPeople()
        {
            using var db = NewDB();
            return db.People.ToList();
        }

        AdvWorkDbContext NewDB() => _dbfact.CreateDbContext();
    }
}
