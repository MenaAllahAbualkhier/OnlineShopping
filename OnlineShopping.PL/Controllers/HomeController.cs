using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.BL.Interface;
using OnlineShopping.BL.Model;
using OnlineShopping.DAL.Entity;

namespace OnlineShopping.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Cons
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        #endregion
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public IActionResult SubCategory(int Id)
        {
            var data = unitOfWork.SubCategory.GetAll(a =>a.CategoryId == Id);
            var model = mapper.Map<IEnumerable<SubCategoryVM>>(data);
            return View(model);
        }

        public IActionResult Item(int Id)
        {
            var data = unitOfWork.Item.GetAll(a => a.SubCategoryId == Id);
            var model = mapper.Map<IEnumerable<ItemVM>>(data);
            return View(model);
        }

        public IActionResult Details(int Id)
        {
            var data = unitOfWork.Item.GetById(a => a.Id == Id);
            var model = mapper.Map<ItemVM>(data);
            return View(model);
        }

    }
}
