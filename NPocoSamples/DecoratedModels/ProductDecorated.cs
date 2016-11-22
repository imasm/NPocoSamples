using NPoco;

namespace NPocoSamples.DecoratedModels
{
    [TableName("Products")]
    [PrimaryKey("ProductId")]
    public class ProductDecorated
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public override string ToString()
        {
            return $"{ProductId,2} - {ProductName}, (Cat: {CategoryId})";
        }
    }
}