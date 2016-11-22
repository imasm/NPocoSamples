using System.Collections.Generic;
using System.Linq;
using NPoco;
using NPocoSamples.Common;
using NPocoSamples.DecoratedModels;
using NUnit.Framework;

namespace NPocoSamples.DecoratedTests
{
    [TestFixture]
    public class SimpleQueryDecoratedTests : BaseTests
    {
        [Test]
        public void Test_SingleById()
        {
            using (var db = new Database(DbInfo.Name))
            {
                var result = db.SingleById<ProductDecorated>(1);
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
                    db.SingleById<ProductDecorated>(9999);
                }
            }, Throws.Exception);
        }

        [Test]
        public void Test_SingleOrDefaultById()
        {
            using (var db = new Database(DbInfo.Name))
            {
                var result = db.SingleOrDefaultById<ProductDecorated>(9999);
                Assert.That(result, Is.Null);
            }
        }

        [Test]
        public void Test_Fetch()
        {
            using (var db = new Database(DbInfo.Name))
            {
                List<ProductDecorated> result = db.Fetch<ProductDecorated>();
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
                IEnumerable<ProductDecorated> result = db
                    .Query<ProductDecorated>()
                    .ToEnumerable();
                Output(result);
            }
        }


        [Test]
        public void Test_Query_WithFilter()
        {
            using (var db = new Database(DbInfo.Name))
            {
                List<ProductDecorated> result = db
                    .Query<ProductDecorated>()
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
                List<ProductDecorated> result = db
                    .Query<ProductDecorated>()
                    .OrderBy(x => x.ProductId)
                    .ToList();
                Output(result);

                List<ProductDecorated> orderedList = result
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
                List<ProductDecorated> result = db
                    .Query<ProductDecorated>()
                    .OrderByDescending(x => x.ProductId)
                    .ToList();

                Output(result);

                List<ProductDecorated> orderedList = result
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
                List<ProductDecorated> result = db
                    .Query<ProductDecorated>()
                    .OrderBy(x => x.CategoryId)
                    .ThenBy(x => x.ProductId)
                    .ToList();

                Output(result);

                List<ProductDecorated> orderedList = result
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
                List<ProductDecorated> result = db
                    .Query<ProductDecorated>()
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
                    .Query<ProductDecorated>()
                    .Where(x=>x.CategoryId == 1)
                    .Count();

                Assert.That(result, Is.EqualTo(12));
            }
        }

        private void AssertIsProduct1(ProductDecorated product)
        {
            Assert.That(product.ProductId, Is.EqualTo(1));
            Assert.That(product.ProductName, Is.EqualTo("Chai"));
            Assert.That(product.CategoryId, Is.EqualTo(1));
            Assert.That(product.UnitPrice.GetValueOrDefault(), Is.EqualTo(18.0m));
        }

    }
}
