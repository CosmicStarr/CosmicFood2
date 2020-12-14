using CosmicFood2.Data.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Data.IRepository.Repository
{
    public class FoodTypeRepository : Repository<FoodType>, IFoodTypeRepository
    {
        private readonly ApplicationDbContext _db;
        public FoodTypeRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
        public IEnumerable<SelectListItem> GetListForDropDown()
        {
            return _db.GetFoodTypes.Select(f => new SelectListItem()
            {
                Text = f.Name,
                Value = f.ID.ToString()
            });
        }

        public void Update(FoodType foodType)
        {
            var objfromdb = _db.GetFoodTypes.FirstOrDefault(f => f.ID == foodType.ID);
            objfromdb.Name = foodType.Name;
            _db.SaveChanges();
        }
    }
}
