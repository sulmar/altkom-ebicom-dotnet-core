namespace Altkom.DotnetCore.Models
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string BarCode { get; set; }
        public string Color { get; set; }
    }
}
