using CourseWorkWeb.Models;

namespace CourseWorkWeb.Data;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product  product);
}