using DAL.Models;

namespace DAL.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        Category GetById(int id);
        IEnumerable<Category> GetAll();
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);

        bool IsCategoryNameExists(string name);
    }
}
