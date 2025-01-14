using JWTAuthCoreAPIRestful.Models;

namespace JWTAuthCoreAPIRestful.Interface
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProduct(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
    }
}
