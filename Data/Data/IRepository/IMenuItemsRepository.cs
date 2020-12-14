using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Data.IRepository
{
    public interface IMenuItemsRepository:IRepository<MenuItems>
    {
        void Update(MenuItems menuItems);
    }
}
