using CosmicFood2.Data.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Data.IRepository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            CategoryRepository = new CategoryRepository(db);
            FoodTypeRepository = new FoodTypeRepository(db);
            MenuItemsRepository = new MenuItemsRepository(db);
        }
        public ICategoryRepository CategoryRepository { get; private set; }

        public IFoodTypeRepository FoodTypeRepository { get; private set; }

        public IMenuItemsRepository MenuItemsRepository { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
