using CosmicFood2.Data.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Data.IRepository.Repository
{
    public class CategoryRepository:Repository<Category>,ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListForDropDown()
        {
            return _db.GetCategories.Select(s => new SelectListItem()
            {
                Text = s.Name,
                Value = s.Id.ToString()
            });
        }

        public void Update(Category category)
        {
            var objfromdb = _db.GetCategories.FirstOrDefault(c => c.Id == category.Id);
            objfromdb.Name = category.Name;
            objfromdb.DisplayOrder = category.DisplayOrder;
            _db.SaveChanges();
        }
    }
}
