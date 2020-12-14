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
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //return Json(new {data = _unitOfWork.SP_Call.ReturnList<Category>("",null) });
            return Json(new { data = _unitOfWork.CategoryRepository.GetAll() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objfromDb = _unitOfWork.CategoryRepository.GetFirstOrDefault(c => c.Id == id);
            if (objfromDb == null)
            {
                return Json(new { success = false, message = "Somthing went completely wrong while deleting" });
            }
            _unitOfWork.CategoryRepository.Remove(objfromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Wonderingful! Its gone." });

        }
    }
}
