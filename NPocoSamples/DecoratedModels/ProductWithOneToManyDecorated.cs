using NPoco;

namespace NPocoSamples.DecoratedModels
{
    [TableName("Products")]
    [PrimaryKey("ProductId")]
    public class ProductWithForeingDecorated
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        // public int? CategoryId { get; set; } Quit with one-to-many reference
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        // Many-To-One
        [Reference(ReferenceType.Foreign, ColumnName = "CategoryId", ReferenceMemberName = "CategoryId")]
        public CategoryDecorated Category { get; set; }
    }
}