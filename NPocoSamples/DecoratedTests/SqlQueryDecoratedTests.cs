using System.Collections.Generic;
using System.Linq;
using NPoco;
using NPocoSamples.Common;
using NPocoSamples.DecoratedModels;
using NUnit.Framework;

namespace NPocoSamples.DecoratedTests
{
    [TestFixture]
    public class SqlQueryDecoratedTests : BaseTests
    {
        [Test]
        public void Test_First()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                ProductDecorated result = db.First<ProductDecorated>("select * from products where ProductId = @0", 1);
                Assert.That(result, Is.Not.Null);
                AssertIsProduct1(result);
            }
        }

        [Test]
        public void Test_First_Exception()
        {
            Assert.That(() =>
            {
                using (var db = new TestDatabase(DbInfo.Name))
                {
                    db.First<ProductDecorated>("select * from products where ProductId = @0", 9999);
                }
            }, Throws.Exception);
        }

        [Test]
        public void Test_FirstOrDefault()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                var result = db.FirstOrDefault<ProductDecorated>("select * from products where ProductId = @0", 9999);
                Assert.That(result, Is.Null);
            }
        }

        [Test]
        public void Test_Fetch()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                List<ProductDecorated> result = db.Fetch<ProductDecorated>("select * from products");
                Output(result);

                Assert.That(result.Count, Is.EqualTo(77));
                AssertIsProduct1(result.First(x => x.CategoryId == 1));
            }
        }

        [Test]
        public void Test_Query()
        {
            using (var db = new TestDatabase(DbInfo.Name))
            {
                IEnumerable<ProductDecorated> result = db.Query<ProductDecorated>("select * from products");
                Output(result);
            }
        }
        
        [Test]
        public void Test_Query_WithFilter()
        {
            using (var db = new Database(DbInfo.Name))
            {
                List<ProductDecorated> result = db.Query<ProductDecorated>("select * from products where CategoryId=@0", 1)
                    .ToList();

                Output(result);

                Assert.That(result.Count, Is.EqualTo(12));
                Assert.That(result.All(x => x.CategoryId == 1), Is.True);
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
