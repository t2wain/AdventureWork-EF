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

            var cnt = db.Employees.Count();
            Assert.True(cnt > 0);

            cnt = db.People.Count();
            Assert.True(cnt > 0);
        }
    }
}