using NPocoSamples.Common;
using NPocoSamples.DecoratedModels;
using NUnit.Framework;

namespace NPocoSamples.DecoratedTests
{
    [TestFixture]
    public class LinqQueriesDecoratedTests
    {
        [Test]
        public void Include_ManyToOne_SingleObject()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                ProductWithForeingDecorated product = db.Query<ProductWithForeingDecorated>()
                    .Include(x => x.Category)
                    .FirstOrDefault(x=>x.ProductId == 1);

                Assert.That(product, Is.Not.Null);
                Assert.That(product.Category, Is.Not.Null);
            }
        }

        [Test]
        public void Include_OneToOne_SingleObject()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                ProductWithOneToOneDecorated product = db.Query<ProductWithOneToOneDecorated>()
                    .Include(x => x.Category)
                    .FirstOrDefault(x => x.ProductId == 1);

                Assert.That(product, Is.Not.Null);
                Assert.That(product.Category, Is.Not.Null);
                Assert.That(product.CategoryId, Is.EqualTo(product.Category.CategoryId));
            }
        }

        [Test]
        public void Include_OneToMany_SingleObject()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                Order order = db.Query<Order>()
                    .IncludeMany(x => x.Details)
                    .FirstOrDefault(x => x.OrderId == 10248);

                Assert.That(order, Is.Not.Null);
                Assert.That(order.Details, Is.Not.Null);
                Assert.That(order.Details.Count, Is.EqualTo(3));
            }
        }
    }
}
