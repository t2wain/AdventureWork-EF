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
        public void EntityCount()
        {
            using var db = _ctx.NewDB();

            var cnt = db.VProductAndDescriptions.Count();
            Assert.True(cnt > 0);

            cnt = db.VProductModelCatalogDescriptions.Count();
            Assert.True(cnt > 0);

            cnt = db.VSalesPeople.Count();
            Assert.True(cnt > 0);
        }

        [Fact]
        public void EmployeeCount()
        {
            var cnt = _ctx.Repo.GetEmployees().Count();
            Assert.True(cnt > 0);
        }

        [Fact]
        public void PeopleCount()
        {
            var cnt = _ctx.Repo.GetPeople().Count();
            Assert.True(cnt > 0);
        }
    }
}