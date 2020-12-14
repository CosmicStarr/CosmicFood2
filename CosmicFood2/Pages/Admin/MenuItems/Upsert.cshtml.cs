using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.Data.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Models.ViewModels;

namespace CosmicFood2.Pages.Admin.MenuItems
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public MenuItemsViewModel MenuItems { get; set; }

        public IActionResult OnGet(int? id)
        {
            MenuItems = new MenuItemsViewModel
            {
                CategoryList = _unitOfWork.CategoryRepository.GetListForDropDown(),
                FoodTypeList = _unitOfWork.FoodTypeRepository.GetListForDropDown(),
                MenuItems = new Models.MenuItems()
            };
            if (id != null)
            {
                MenuItems.MenuItems = _unitOfWork.MenuItemsRepository.GetFirstOrDefault(u => u.ID == id);
                if (MenuItems.MenuItems == null)
                {
                    return NotFound();
                }
            }
            return Page();

        }


        public IActionResult OnPost()
        {

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid)
            {
                MenuItems = new MenuItemsViewModel
                {
                    CategoryList = _unitOfWork.CategoryRepository.GetListForDropDown(),
                    FoodTypeList = _unitOfWork.FoodTypeRepository.GetListForDropDown(),
                    MenuItems = new Models.MenuItems()
                };

                return Page();
            }
            if (MenuItems.MenuItems.ID == 0)
            {
                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"Images\MenuItems");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }
                MenuItems.MenuItems.Image = @"\Images\MenuItems\" + fileName + extension;

                _unitOfWork.MenuItemsRepository.Add(MenuItems.MenuItems);
            }
            else
            {
                //Edit Menu Item
                var objFromDb = _unitOfWork.MenuItemsRepository.Get(MenuItems.MenuItems.ID);
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"Images\MenuItems");
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));

                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }


                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    MenuItems.MenuItems.Image = @"\Images\MenuItems\" + fileName + extension;
                }
                else
                {
                    MenuItems.MenuItems.Image = objFromDb.Image;
                }


                _unitOfWork.MenuItemsRepository.Update(MenuItems.MenuItems);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }

}

