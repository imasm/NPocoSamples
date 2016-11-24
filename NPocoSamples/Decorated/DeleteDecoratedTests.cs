using NPoco;
using NPocoSamples.Common;
using NPocoSamples.Decorated.Models;
using NUnit.Framework;

namespace NPocoSamples.Decorated
{
    [TestFixture]
    public class DeleteDecoratedTests: BaseTests
    {
        [OneTimeSetUp]
        [OneTimeTearDown]
        public void DeleteAddedCustomer()
        {
            Output("Delete added customer");
            using (var db = new Database(DbInfo.Name))
            {
                db.BeginTransaction();
                db.ExecuteScalar<int>("delete from customers where customerId = 'DEMO'");
                db.CompleteTransaction();
            }
        }

        [SetUp]
        public void InsertCustomerToUpdate()
        {
            Output("Insert customer to delete");
            using (var db = new Database(DbInfo.Name))
            {
                db.BeginTransaction();
                var customer = new Customer()
                {
                    CustomerId = "DEMO",
                    CompanyName = "My company",
                    ContactTitle = "Mr",
                    ContactName = "John Smith",
                };
                db.Insert(customer);
                db.CompleteTransaction();
            }
        }

        [Test]
        public void Delete_Object()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                db.BeginTransaction();
                var customer = db.SingleById<Customer>("DEMO");
                int deleted = db.Delete(customer);
                db.CompleteTransaction();

                Assert.That(deleted, Is.EqualTo(1));
            }

            AssertIsDeleted("DEMO");
        }

        [Test]
        public void Delete_ByPrimaryKey()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                db.BeginTransaction();
                int deleted = db.Delete<Customer>((object)"DEMO");
                db.CompleteTransaction();

                Assert.That(deleted, Is.EqualTo(1));
            }

            AssertIsDeleted("DEMO");
        }

        [Test]
        public void Delete_ByWhere()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                db.BeginTransaction();
                int deleted = db.Delete<Customer>("where CustomerId = @0", "DEMO");
                db.CompleteTransaction();

                Assert.That(deleted, Is.EqualTo(1));
            }

            AssertIsDeleted("DEMO");
        }


        private void AssertIsDeleted(string customerId)
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                var customer = db.SingleOrDefaultById<Customer>(customerId);
                Assert.That(customer, Is.Null);
            }
        }        
    }
}