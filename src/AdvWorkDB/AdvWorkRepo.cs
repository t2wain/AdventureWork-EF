using AdvWorkDB.OtherEntity;
using AdvWorkEntity.Entity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AdvWorkDB
{
    public class AdvWorkRepo
    {
        IDbContextFactory<AdvWorkDbContext> _dbfact;

        public AdvWorkRepo(IDbContextFactory<AdvWorkDbContext> dbfact)
        {
            this._dbfact = dbfact;
        }

        #region Basic Queries

        /// <summary>
        /// Async method using async/await
        /// </summary>
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            using var db = NewDB();
            return await db.Employees.ToListAsync();
        }

        /// <summary>
        /// Async method without using async/await
        /// </summary>
        public Task<IEnumerable<Person>> GetPeople()
        {
            var db = NewDB();
            return db.People
                .Take(1000)
                .Include(p => p.EmailAddresses)
                .Include(p => p.Employee)
                .ToListAsync()
                .ContinueWith(t => 
                {
                    db.Dispose();
                    var data = t.Result;
                    if (t.IsFaulted)
                        // must always check for exception
                        // and return it along with this new Task
                        // otherwise the inner exception will be lost.
                        throw t.Exception!.Flatten();
                    else return data.AsEnumerable();
                });
        }

        #endregion

        #region SQL / Stored Procedure

        public async Task<IEnumerable<Employee>> GetEmployeesSql()
        {
            using var db = NewDB();
            return await db.Employees
                .FromSqlRaw(@"select * from HumanResources.Employee")
                .ToListAsync();
        }

        /// <summary>
        /// Safe execution of SQL/SP using format-string
        /// </summary>
        public async Task<IEnumerable<BillOfMaterialSP>> GetBillOfMaterial()
        {
            using var db = NewDB();
            return await db.BillOfMaterialSP
                .FromSqlRaw("EXECUTE dbo.uspGetBillOfMaterials @StartProductID={0}, @CheckDate={1}", 
                    316, new DateTime(2026,01,01)) // this is safe from SQL injection
                .ToListAsync();
        }

        /// <summary>
        /// Safe execution of SQL/SP using DbParameter
        /// </summary>
        public async Task<IEnumerable<BillOfMaterialSP>> GetBillOfMaterial2()
        {
            IDbDataParameter[] lstParams = [
                    NewParam("checkDate", SqlDbType.DateTime, new DateTime(2026, 01, 01)),
                    NewParam("prodId", SqlDbType.Int, 316),
                ]; 

            using var db = NewDB();
            return await db.BillOfMaterialSP
                .FromSqlRaw("EXECUTE dbo.uspGetBillOfMaterials @prodId, @checkDate",
                   lstParams) // this is safe from SQL injection
                .ToListAsync();
        }

        /// <summary>
        /// Safe execution of SQL/SP with interpolated string
        /// </summary>
        public async Task<IEnumerable<BillOfMaterialSP>> GetBillOfMaterial3()
        {
            var prodId = 316;
            var checkDate = new DateTime(2026, 01, 01);

            using var db = NewDB();
            return await db.BillOfMaterialSP
                // internally, it uses DbParameter
                .FromSqlInterpolated($"EXECUTE dbo.uspGetBillOfMaterials {prodId}, {checkDate}") 
                .ToListAsync();
        }

        public async Task<IEnumerable<ContactInfoSP>> GetContactInfo(int personId)
        {
            using var db = NewDB();
            return await db.ContactInfoSP
                .FromSqlRaw($"select * from dbo.ufnGetContactInformation({personId})")
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetContactInfo2(int personId)
        {
            using var db = NewDB();
            var q = from pi in db.GetContactInformation(1) // user-defined table-valued function
                    join p in db.People on pi.PersonID equals p.BusinessEntityId
                    select p;
            return await q.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetLowStockProducts(int quantityThreshold)
        {
            using var db = NewDB();
            var q = from p in db.Products
                    where db.GetStock(p.ProductId) < quantityThreshold // user-defined function
                    select p;
            return await q.ToListAsync();
        }

        #endregion

        #region Using Transaction

        public async Task<int> TaskWithTransaction()
        {
            using var db = NewDB();
            using var tran = await db.Database.BeginTransactionAsync();
            try
            {
                var cnt = await Task.Run(() => OtherTransactionalTask(db));
                //await db.SaveChangesAsync();
                await tran.CommitAsync();
                return cnt;
            }
            catch
            {
                await tran.RollbackAsync();
                throw;
            }
        }

        public Task<int> TaskWithTransaction2() =>
            Task.Run(() =>
            {
                using var db = NewDB();
                using var tran = db.Database.BeginTransaction();
                try
                {
                    var cnt = OtherTransactionalTask(db);
                    //db.SaveChanges();
                    tran.Commit();
                    return cnt;
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            });

        public Task<int> TaskWithTransaction3()
        {
            var db = NewDB();
            return db.Database.BeginTransactionAsync()
                .ContinueWith(t => 
                {
                    var tran = t.Result;
                    try
                    {
                        var cnt = OtherTransactionalTask(db);
                        //db.SaveChanges();
                        tran.Commit();
                        return cnt;
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                    finally
                    {
                        tran.Dispose();
                        db.Dispose();
                    }
                });
        }

        protected int OtherTransactionalTask(AdvWorkDbContext db) => 
            db.Employees.Count();

        #endregion

        /// <summary>
        /// Generalize the creation of parameter for specific data provider
        /// </summary>
        virtual protected IDbDataParameter NewParam(string name, SqlDbType type, object value)
        {
            var p = new SqlParameter(name, type);
            p.Value = value;
            return p;
        }

        AdvWorkDbContext NewDB() => _dbfact.CreateDbContext();
    }
}
