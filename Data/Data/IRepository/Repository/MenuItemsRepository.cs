using CosmicFood2.Data.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Data.IRepository.Repository
{
    public class MenuItemsRepository : Repository<MenuItems>, IMenuItemsRepository
    {
        private readonly ApplicationDbContext _db;
        public MenuItemsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(MenuItems menuItems)
        {
            var objfromdb = _db.GetMenuItems.FirstOrDefault(g => g.ID == menuItems.ID);
            objfromdb.Name = menuItems.Name;
            objfromdb.Description = menuItems.Description;
            objfromdb.Price = menuItems.Price;
            objfromdb.CategoryID = menuItems.CategoryID;
            objfromdb.FoodTypeID = menuItems.FoodTypeID;
            if(objfromdb.Image != null)
            {
                objfromdb.Image = menuItems.Image;
            }
            _db.SaveChanges();
        }
    }
}
