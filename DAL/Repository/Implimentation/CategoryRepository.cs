using DAL.Data;
using DAL.Models;
using DAL.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Repository.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var obj = _context.Categories.Find(id);
            if (obj != null)
            {
                _context.Categories.Remove(obj);
                _context.SaveChanges();
            }
        }

        public Category GetById(int id)
        {
            return _context.Categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public bool IsCategoryNameExists(string name)
        {
            return _context.Categories.Any(x => x.CategoryName == name);
        }
    }
}
