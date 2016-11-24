using System;
using NPoco;
using NPocoSamples.Common;
using NPocoSamples.Decorated.Models;
using NUnit.Framework;

namespace NPocoSamples.Decorated
{
    [TestFixture]
    public class InsertDecoratedTests: BaseTests
    {
        [OneTimeSetUp]
        [OneTimeTearDown]
        public void DeleteInserts()
        {
            Output("Delete inserted data");
            using (var db = new Database(DbInfo.Name))
            {
                db.BeginTransaction();
                db.ExecuteScalar<int>("delete from categories where categoryId > 8");
                db.ExecuteScalar<int>("delete from customers where customerId = 'DEMO'");
                db.ExecuteScalar<int>("DBCC CHECKIDENT (categories, RESEED, 8)");
                db.CompleteTransaction();
            }
        }

        [Test]
        public void Insert_With_PrimaryKey_Auto()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                var category = new Category()
                {
                    //CategoryId = 9, this is ignored becouse AutoIncrement is defined in the model
                    CategoryName = "Custom Category",
                    Description = "Sample category",
                    Picture = null,
                };

                db.BeginTransaction();
                object result = db.Insert(category);
                db.CompleteTransaction();


                int primaryKey = Convert.ToInt32(result);
                
                Output("Category inserted with Id = " + primaryKey);
                Assert.That(primaryKey, Is.GreaterThan(8));
                Assert.That(primaryKey, Is.EqualTo(category.CategoryId));
            }
        }

        [Test]
        public void Insert_With_PrimaryKey_Custom()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                db.BeginTransaction();
                var customer = new Customer()
                {
                    CustomerId = "DEMO",
                    CompanyName = "My company"
                };

                object result = db.Insert(customer);
                string insertedId = Convert.ToString(result);

                Output("Customer inserted with Id = " + insertedId);
                Assert.That(insertedId, Is.EqualTo(customer.CustomerId));

                db.CompleteTransaction();
            }
        }
    }
}
