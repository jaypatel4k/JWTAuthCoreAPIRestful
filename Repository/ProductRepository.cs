using JWTAuthCoreAPIRestful.Data;
using JWTAuthCoreAPIRestful.Interface;
using JWTAuthCoreAPIRestful.Models;


namespace JWTAuthCoreAPIRestful.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly JWTAuthCRUDContext _dbcontext;
        public ProductRepository(JWTAuthCRUDContext dbcontext)
        {
            _dbcontext = dbcontext; 
        }
        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _dbcontext.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            return _dbcontext.Products.First(x=>x.Id == id);
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
