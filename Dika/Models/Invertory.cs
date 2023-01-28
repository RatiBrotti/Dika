namespace Dika.Models
{
    public class Invertory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SKU { get; set; }
        public string Barcode { get; set; }
        public string QuantityOfProvider { get; set; }
        public string QuantityCounted { get; set; }
        public string Price { get; set; }
    }
}
