using NPoco;

namespace NPocoSamples.DecoratedModels
{
    [TableName("Products")]
    [PrimaryKey("ProductId")]
    public class ProductWithOneToOneDecorated
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; } // With one-to-one, this is allowed
        public string QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }
        
        [Reference(ReferenceType.OneToOne, ColumnName = "CategoryId", ReferenceMemberName = "CategoryId")]
        public CategoryDecorated Category { get; set; }
    }
}