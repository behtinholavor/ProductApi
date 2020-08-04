namespace product.stock.api
{
    public interface IStockRepository : IRepository<Product>
    {
        UserVM Insert(User user);
        UserVM Login(User auth);
    }
}
