using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.Data.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CosmicFood2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public MenuItemsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.MenuItemsRepository.GetAll(null, null, "Category,FoodType") });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int? id)
        {
            try
            {
                var objfromDb = _unitOfWork.MenuItemsRepository.GetFirstOrDefault(c => c.ID == id);
                if (objfromDb == null)
                {
                    return Json(new { success = false, message = "Somthing went completely wrong while deleting" });
                }

                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, objfromDb.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
                _unitOfWork.MenuItemsRepository.Remove(objfromDb);
                _unitOfWork.Save();
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Somthing went completely wrong while deleting" });
            }

            return Json(new { success = true, message = "Wonderingful! Its gone." });
        }
    }
}
