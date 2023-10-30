using CourseWorkWeb.Models;

namespace CourseWorkWeb.Data;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category  category);
}