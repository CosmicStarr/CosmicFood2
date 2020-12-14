using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Data.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CosmicFood2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public FoodTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.FoodTypeRepository.GetAll() });
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _unitOfWork.FoodTypeRepository.GetFirstOrDefault(u => u.ID == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting." });
            }
            _unitOfWork.FoodTypeRepository.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete success." });
        }

    }
}
