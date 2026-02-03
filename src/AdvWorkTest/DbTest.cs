using Microsoft.EntityFrameworkCore;

namespace AdvWorkTest
{
    public class DbTest : IClassFixture<Context>
    {
        Context _ctx;

        public DbTest(Context ctx)
        {
            this._ctx = ctx;
        }

        [Fact]
        public async void EntityCount()
        {
            using var db = _ctx.NewDB();

            var cnt = await db.VProductAndDescriptions.CountAsync();
            Assert.True(cnt > 0);

            cnt = await db.VProductModelCatalogDescriptions.CountAsync();
            Assert.True(cnt > 0);

            cnt = await db.VSalesPeople.CountAsync();
            Assert.True(cnt > 0);
        }

        [Fact]
        public async void EmployeeCount()
        {
            var employees = await _ctx.Repo.GetEmployees();
            var cnt = employees.Count();
            Assert.True(cnt > 0);
        }

        [Fact]
        public async void PeopleCount()
        {
            var people = await _ctx.Repo.GetPeople();

            var cnt = people.Count();
            Assert.True(cnt > 0);

            cnt = people
                .Where(p => p.Employee != null)
                .Count();
            Assert.True(cnt > 0);

            cnt = people
                .Where(p => p.EmailAddresses.Count() > 0)
                .Count();
            Assert.True(cnt > 0);
        }

        [Fact]
        public async void UseTransaction()
        {
            var cnt = await _ctx.Repo.TaskWithTransaction();
            Assert.True(cnt > 0);

            cnt = await _ctx.Repo.TaskWithTransaction2();
            Assert.True(cnt > 0);

            cnt = await _ctx.Repo.TaskWithTransaction3();
            Assert.True(cnt > 0);
        }

        [Fact]
        public async void EmployeeCountSql()
        {
            var employees = await _ctx.Repo.GetEmployeesSql();
            var cnt = employees.Count();
            Assert.True(cnt > 0);
        }

        [Fact]
        public async void BillOfMaterialSP()
        {
            var bom = await _ctx.Repo.GetBillOfMaterial();
            bom = await _ctx.Repo.GetBillOfMaterial2();
            bom = await _ctx.Repo.GetBillOfMaterial3();
        }

        [Fact]
        public async void ContsactInfoSP()
        {
            var pinfo = (await _ctx.Repo.GetContactInfo(1)).FirstOrDefault();
            Assert.NotNull(pinfo);

            var person = (await _ctx.Repo.GetContactInfo2(1)).FirstOrDefault();
            Assert.NotNull(person);
        }

        [Fact]
        public async void LowStockProduct()
        {
            var products = await _ctx.Repo.GetLowStockProducts(5);
            var cnt = products.Count();
            Assert.True(cnt > 0);
        }

    }
}