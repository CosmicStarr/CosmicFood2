using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Data.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CosmicFood2.Pages.Admin.FoodType
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Models.FoodType FoodType { get; set; }
        public IActionResult OnGet(int? id)
        {
            FoodType = new Models.FoodType();
            if (id != null)
            {
                FoodType = _unitOfWork.FoodTypeRepository.GetFirstOrDefault(f => f.ID == id.GetValueOrDefault());
                if (FoodType == null)
                {
                    NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (FoodType.ID == 0)
            {
                _unitOfWork.FoodTypeRepository.Add(FoodType);
            }
            else
            {
                _unitOfWork.FoodTypeRepository.Update(FoodType);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

    }
}
