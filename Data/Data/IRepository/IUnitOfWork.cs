using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Data.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        IFoodTypeRepository FoodTypeRepository { get; }
        IMenuItemsRepository MenuItemsRepository { get; }

        void Save();
    }
}
