using System;

namespace product.stock.api
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Token { get; set; }
        public DateTime? Validity { get; set; }
    }
}
