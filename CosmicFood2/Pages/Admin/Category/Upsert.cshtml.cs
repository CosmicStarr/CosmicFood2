using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Data.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CosmicFood2.Pages.Admin.Category
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public Models.Category category { get; set; }
        public IActionResult OnGet(int? id)
        {
            category = new Models.Category();
            if (id != null)
            {
                category = _unitOfWork.CategoryRepository.GetFirstOrDefault(u => u.Id == id.GetValueOrDefault());
                if (category == null)
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
            if (category.Id == 0)
            {
                _unitOfWork.CategoryRepository.Add(category);
            }
            else
            {
                _unitOfWork.CategoryRepository.Update(category);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }

    }
}
