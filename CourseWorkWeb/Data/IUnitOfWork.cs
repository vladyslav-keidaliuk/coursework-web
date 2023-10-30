namespace CourseWorkWeb.Data;

public interface IUnitOfWork
{
     ICategoryRepository Category { get; }

     void Save();
}