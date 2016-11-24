using NPoco;

namespace NPocoSamples.Decorated.Models
{
    [TableName("Order details")]
    [PrimaryKey(new [] { "OrderId", "ProductId" }, AutoIncrement = false)]
    public class OrderDetails
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public double Discount { get; set; }
    }
}