using NPoco;

namespace NPocoSamples.Decorated.Models
{
    [TableName("Categories")]
    [PrimaryKey("CategoryId")]
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public override string ToString()
        {
            return $"{CategoryId,2} - {CategoryName}";
        }
    }
}