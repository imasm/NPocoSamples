using System.Collections.Generic;
using NPoco;
using NPocoSamples.Common;
using NPocoSamples.Decorated.Models;
using NUnit.Framework;

namespace NPocoSamples.Decorated
{
    [TestFixture]
    public class UpdateDecoratedTests: BaseTests
    {
        [OneTimeSetUp]
        public void InsertCustomerToUpdate()
        {
            Output("Insert customer to update");
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

        [Test]
        public void UpdateCustomer()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                db.BeginTransaction();
                var customer = db.SingleById<Customer>("DEMO");
                customer.CompanyName = "Second company";
                customer.ContactName = "Michael Lauson";

                int rowsUpdated = db.Update(customer);
                Output("Items updated = " + rowsUpdated);
                
            
                customer = db.SingleById<Customer>("DEMO");

                Assert.That(customer.CompanyName, Is.EqualTo("Second company"));
                Assert.That(customer.ContactName, Is.EqualTo("Michael Lauson"));

                db.CompleteTransaction();
            }
        }


        [Test]
        public void UpdateCustomer_ExplicitColumns()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                db.BeginTransaction();
                var customer = db.SingleById<Customer>("DEMO");
                string originalContactName = customer.ContactName;

                customer.CompanyName = "Third company";
                customer.ContactName = "Christian Noe";

                int rowsUpdated = db.Update(customer, new[] { "CompanyName" });
                db.CompleteTransaction();
                
                Output("Items updated = " + rowsUpdated);

                customer = db.SingleById<Customer>("DEMO");

                Assert.That(customer.CompanyName, Is.EqualTo("Third company"));
                Assert.That(customer.ContactName, Is.EqualTo(originalContactName));
            }
        }


        [Test]
        public void UpdateCustomer_WithSnapshot()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                db.BeginTransaction();
                var customer = db.SingleById<Customer>("DEMO");

                Snapshot<Customer> snapshot = db.StartSnapshot(customer);

                customer.CompanyName = "Four company";
                customer.ContactName = "Oliver Hassel";

                List<string> updatedColumns = snapshot.UpdatedColumns();

                Assert.That(updatedColumns, Contains.Item("CompanyName"));
                Assert.That(updatedColumns, Contains.Item("ContactName"));
                
                int rowsUpdated = db.Update(customer, updatedColumns);
                db.CompleteTransaction();

                Output("Items updated = " + rowsUpdated);
            }
        }
    }
}