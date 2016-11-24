using System.Collections.Generic;
using System.Linq;
using NPoco;
using NPocoSamples.Common;
using NPocoSamples.Decorated.Models;
using NUnit.Framework;

namespace NPocoSamples.Decorated
{
    [TestFixture]
    public class SimpleQueryDecoratedTests : BaseTests
    {
        [Test]
        public void Test_SingleById()
        {
            using (var db = new Database(DbInfo.Name))
            {
                var result = db.SingleById<Product>(1);
                Assert.That(result, Is.Not.Null);
                AssertIsProduct1(result);
            }
        }

        [Test]
        public void Test_SingleById_Exception()
        {
            Assert.That(() =>
            {
                using (var db = new Database(DbInfo.Name))
                {
                    db.SingleById<Product>(9999);
                }
            }, Throws.Exception);
        }

        [Test]
        public void Test_SingleOrDefaultById()
        {
            using (var db = new Database(DbInfo.Name))
            {
                var result = db.SingleOrDefaultById<Product>(9999);
                Assert.That(result, Is.Null);
            }
        }

        [Test]
        public void Test_Fetch()
        {
            using (var db = new Database(DbInfo.Name))
            {
                List<Product> result = db.Fetch<Product>();
                Output(result);

                Assert.That(result.Count, Is.EqualTo(77));
                AssertIsProduct1(result.First(x => x.CategoryId == 1));
            }
        }

        [Test]
        public void Test_Query()
        {
            using (var db = new Database(DbInfo.Name))
            {
                IEnumerable<Product> result = db
                    .Query<Product>()
                    .ToEnumerable();
                Output(result);
            }
        }


        [Test]
        public void Test_Query_WithFilter()
        {
            using (var db = new Database(DbInfo.Name))
            {
                List<Product> result = db
                    .Query<Product>()
                    .Where(x => x.CategoryId == 1)
                    .ToList();
                Output(result);

                Assert.That(result.Count, Is.EqualTo(12));
                Assert.That(result.All(x => x.CategoryId == 1), Is.True);
            }
        }

        [Test]
        public void Test_Query_WithOrder()
        {
            using (var db = new Database(DbInfo.Name))
            {
                List<Product> result = db
                    .Query<Product>()
                    .OrderBy(x => x.ProductId)
                    .ToList();
                Output(result);

                List<Product> orderedList = result
                    .OrderBy(x => x.ProductId)
                    .ToList();

                Assert.That(result, Is.EqualTo(orderedList));
            }
        }

        [Test]
        public void Test_Query_WithOrder_Descending()
        {
            using (var db = new Database(DbInfo.Name))
            {
                List<Product> result = db
                    .Query<Product>()
                    .OrderByDescending(x => x.ProductId)
                    .ToList();

                Output(result);

                List<Product> orderedList = result
                    .OrderByDescending(x => x.ProductId)
                    .ToList();

                Assert.That(result, Is.EqualTo(orderedList));
            }
        }

        [Test]
        public void Test_Query_WithOrder_Multiple()
        {
            using (var db = new Database(DbInfo.Name))
            {
                List<Product> result = db
                    .Query<Product>()
                    .OrderBy(x => x.CategoryId)
                    .ThenBy(x => x.ProductId)
                    .ToList();

                Output(result);

                List<Product> orderedList = result
                     .OrderBy(x => x.CategoryId)
                    .ThenBy(x => x.ProductId)
                    .ToList();

                Assert.That(result, Is.EqualTo(orderedList));
            }
        }

        [Test]
        public void Test_Query_First10()
        {
            using (var db = new Database(DbInfo.Name))
            {
                List<Product> result = db
                    .Query<Product>()
                    .Limit(10)
                    .ToList();

                Output(result);
                Assert.That(result.Count, Is.EqualTo(10));
            }
        }

        [Test]
        public void Test_Query_Count()
        {
            using (var db = new Database(DbInfo.Name))
            {
                int result = db
                    .Query<Product>()
                    .Where(x=>x.CategoryId == 1)
                    .Count();

                Assert.That(result, Is.EqualTo(12));
            }
        }

        private void AssertIsProduct1(Product product)
        {
            Assert.That(product.ProductId, Is.EqualTo(1));
            Assert.That(product.ProductName, Is.EqualTo("Chai"));
            Assert.That(product.CategoryId, Is.EqualTo(1));
            Assert.That(product.UnitPrice.GetValueOrDefault(), Is.EqualTo(18.0m));
        }

    }
}
