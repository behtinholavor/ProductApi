using System;

namespace product.stock.api
{
    public class ProductVM
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public string Unit { get; set; }
        public string Category { get; set; }
        public DateTime? Inserted { get; set; }
        public DateTime? Modified { get; set; }
        
    }
}
