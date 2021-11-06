using Cw1_Cloud_7624.DbContexts;
using Cw1_Cloud_7624.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Cw1_Cloud_7624.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ProductDbContext _dbContext;
        public CategoryRepository(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void DeleteCategory(int categoryId)
        {
            var category = _dbContext.Categories.Find(categoryId);
            _dbContext.Categories.Remove(category);
            Save();
        }
        public Category GetCategoryById(int categoryId)
        {
            var category = _dbContext.Categories.Find(categoryId);
            return category;
        }
        public IEnumerable<Category> GetCategories()
        {
            return _dbContext.Categories.ToList();
        }
        public void InsertCategory(Category category)
        {
            _dbContext.Add(category);
            Save();
        }
        public void Save()
        {
            _dbContext.SaveChanges();
        }
        public void UpdateCategory(Category category)
        {
            _dbContext.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            Save();
        }
    }
}
