using Microsoft.AspNetCore.Mvc.Rendering;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Data.IRepository
{
    public interface IFoodTypeRepository :IRepository<FoodType>
    {
        IEnumerable<SelectListItem> GetListForDropDown();
        void Update(FoodType foodType);
    }
}
