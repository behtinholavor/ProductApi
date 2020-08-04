using System.Collections.Generic;
using System.Linq;

namespace product.stock.api
{
    public class StockRepository : Repository<Product>, IStockRepository
    {
        protected StockContext _context;        

        public StockRepository(StockContext context) : base(context) 
        {
            _context = context;
        }

        public UserVM Insert(User user)
        {
            user.Token = StockFactory.BuildToken(user);
            _context.Users.Add(user);
            Save();
            User result = _context.Users.Find(user.Id);
            return result.ToModelVM();
        }

        public UserVM Login(User auth)
        {
            List<User> users = _context.Users
                .Where(c => c.Login.Equals(auth.Login) && c.Pass.Equals(auth.Pass)).ToList();
            return users.FirstOrDefault().ToModelVM();
        }

    }
}
