using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OnlineShopping.BL.Interface;
using OnlineShopping.BL.Model;
using OnlineShopping.DAL.Entity;

namespace OnlineShopping.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        #region Cons
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public CategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        #endregion

        public IActionResult Index()
        {
            var data=unitOfWork.Category.GetAll();
            var model = mapper.Map<IEnumerable<CategoryVM>>(data);
            return View(model);
        }

        #region CreateCategory
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(CategoryVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Category>(model);
                    unitOfWork.Category.Add(data);
                    unitOfWork.Complete();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }

        }
        #endregion


        #region DeleteCategory
       public IActionResult DeleteCategory(int Id)
        {
            var data = unitOfWork.Category.GetById(a => a.Id == Id, "SubCategory");
            var model= mapper.Map<CategoryVM>(data);
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteCategory(CategoryVM model)
        {
            try
            {
                var data = mapper.Map<Category>(model);
                unitOfWork.Category.Delete(data);
                unitOfWork.Complete();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        #endregion


        #region DetailsCategory
        public IActionResult DetailsCategory(int Id)
        {
            var data = unitOfWork.Category.GetById(a => a.Id == Id, "SubCategory");
            var model = mapper.Map<CategoryVM>(data);
            return View(model);
        }

       
        #endregion

        #region EditCategory
        public IActionResult EditCategory(int Id)
        {
            var data = unitOfWork.Category.GetById(a => a.Id == Id);
            var model = mapper.Map<CategoryVM>(data);
            return View(model);
        }
        [HttpPost]
        public IActionResult EditCategory(CategoryVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = mapper.Map<Category>(model);
                    unitOfWork.Category.Update(data);
                    unitOfWork.Complete();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View (model);
            }
        }
        #endregion




    }
}
