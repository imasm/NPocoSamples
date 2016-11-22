using NPoco;
using NUnit.Framework;

namespace NPocoSamples
{
    [TestFixture]
    public class ConnectionTests
    {
        [Test]
        public void TestConnect_WithConnectionName()
        {
            using (var db = new Database(DbInfo.Name))
            {
                int result = db.ExecuteScalar<int>("SELECT 1");
                Assert.AreEqual(1, result);
            }
        }


        [Test]
        public void TestConnect_WithConnectionStringAndProviderName()
        {
            using (var db = new Database(DbInfo.ConnecionString, DbInfo.ProviderName))
            {
                int result = db.ExecuteScalar<int>("SELECT 1");
                Assert.AreEqual(1, result);
            }
        }

        [Test]
        public void TestConnect_WithConnectionStringAndDatabaseType()
        {
            using (var db = new Database(DbInfo.ConnecionString, DbInfo.DatabaseType))
            {
                int result = db.ExecuteScalar<int>("SELECT 1");
                Assert.AreEqual(1, result);
            }
        }
    }
}
