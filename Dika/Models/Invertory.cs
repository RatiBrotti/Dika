namespace Dika.Models
{
    public class Invertory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? SKU { get; set; }
        public string Barcode { get; set; }
        public string? Size { get; set; }
        public int QuantityOfProvider { get; set; }
        public int QuantityCounted { get; set; }
        public decimal Price { get; set; }
    }
}
