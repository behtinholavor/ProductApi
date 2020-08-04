using System;

namespace product.stock.api
{
    public class AuthVM
    {
        public string Token { get; set; }
        public DateTime? Validity { get; set; }
    }
}
