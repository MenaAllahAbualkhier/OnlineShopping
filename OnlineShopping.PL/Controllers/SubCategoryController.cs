using AutoMapper;
using Demo.BL.Helper;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.BL.Interface;
using OnlineShopping.BL.Model;
using OnlineShopping.DAL.Entity;

namespace OnlineShopping.Controllers
{
    public class SubCategoryController : Controller
    {
        #region Cons
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SubCategoryController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        #endregion
        public IActionResult Index(int Id)
        {
            ViewBag.category=Id;
            var data =unitOfWork.SubCategory.GetAll(a=>a.CategoryId==Id);
            var model = mapper.Map<IEnumerable<SubCategoryVM>>(data);
            return View(model);
        }


        #region Create
        public IActionResult Create(int category)
        {
            ViewBag.category = category;
            return View();
        }

        [HttpPost]
        public IActionResult Create(SubCategoryVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ImageName = UploadFile.uploadFile(model.Image, "/wwwroot/Sub-Item/");
                    var data = mapper.Map<SubCategory>(model);
                    unitOfWork.SubCategory.Add(data);
                    unitOfWork.Complete();
                    return RedirectToAction("Index", "SubCategory", new { Id = data.CategoryId });
                }
                return View(model);
            }
            catch
            {
                return View(model);
            }
        }
        #endregion


        #region Delete
        public IActionResult Delete(int Id)
        {
            var data = unitOfWork.SubCategory.GetById(a => a.Id == Id,"Item");
            var model = mapper.Map<SubCategoryVM>(data);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(SubCategoryVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UploadFile.DeleteFile(model.ImageName, "/wwwroot/Sub-Item/");
                    var data = mapper.Map<SubCategory>(model);
                    unitOfWork.SubCategory.Delete(data);
                    unitOfWork.Complete();
                    return RedirectToAction("Index", "SubCategory", new { Id = model.CategoryId });
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

        #region Edit
        public IActionResult Edit(int Id)
        {
            var data = unitOfWork.SubCategory.GetById(a => a.Id == Id, "Item");
            var model = mapper.Map<SubCategoryVM>(data);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(SubCategoryVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Image != null)
                    {
                        UploadFile.DeleteFile(model.ImageName, "/wwwroot/Sub-Item/");
                        model.ImageName = UploadFile.uploadFile(model.Image, "/wwwroot/Sub-Item/");
                    }

                    var data = mapper.Map<SubCategory>(model);
                    unitOfWork.SubCategory.Update(data);
                    unitOfWork.Complete();
                    return RedirectToAction("Index", "SubCategory", new { Id = model.CategoryId });
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


        #region Details

        public IActionResult Details(int Id)
        {
            var data = unitOfWork.SubCategory.GetById(a => a.Id == Id, "Item");
            var model = mapper.Map<SubCategoryVM>(data);
            return View(model);
        }


        #endregion




    }
}
