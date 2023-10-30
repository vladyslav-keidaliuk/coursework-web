namespace CourseWorkWeb.Data;

public interface IUnitOfWork
{
     ICategoryRepository Category { get; }
     IProductRepository Product { get; }

    void Save();
}