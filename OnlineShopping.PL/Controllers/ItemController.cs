using AutoMapper;
using Demo.BL.Helper;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.BL.Interface;
using OnlineShopping.BL.Model;
using OnlineShopping.DAL.Entity;

namespace OnlineShopping.Controllers
{
    public class ItemController : Controller
    {
        #region Cost
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ItemController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        #endregion
        public IActionResult Index(int Id)
        {
            var data = unitOfWork.Item.GetAll(a => a.SubCategoryId== Id , "SubCategory");
            var model = mapper.Map<IEnumerable<ItemVM>>(data);
            ViewBag.subId = Id;
            return View(model);
        }

        #region Create
        public IActionResult Create(int SubId)
        {
            ViewBag.SubId= SubId;
            return View();
        }

        [HttpPost]
        public IActionResult Create(ItemVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.ImageName = UploadFile.uploadFile(model.Image, "/wwwroot/Item/");
                    var data = mapper.Map<Item>(model);
                    unitOfWork.Item.Add(data);
                    unitOfWork.Complete();
                    return RedirectToAction("Index", "Item", new { Id = data.SubCategoryId });
                }
                return View(model);
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
            var data = unitOfWork.Item.GetById(a => a.Id == Id, "SubCategory");
            var model = mapper.Map<ItemVM>(data);
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(ItemVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.Image != null)
                    {
                        UploadFile.DeleteFile(model.ImageName, "/wwwroot/Item/");
                        model.ImageName = UploadFile.uploadFile(model.Image, "/wwwroot/Item/");
                    }

                    var data = mapper.Map<Item>(model);
                    unitOfWork.Item.Update(data);
                    unitOfWork.Complete();
                    return RedirectToAction("Index", "Item", new { Id = model.SubCategoryId });
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


        #region Delete
        public IActionResult Delete(int Id)
        {
            var data = unitOfWork.Item.GetById(a => a.Id == Id);
            var model = mapper.Map<ItemVM>(data);
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(Item model)
        {
            try
            {
                
                    UploadFile.DeleteFile(model.ImageName, "/wwwroot/Item/");
                    var data = mapper.Map<Item>(model);
                    unitOfWork.Item.Delete(data);
                    unitOfWork.Complete();
                    return RedirectToAction("Index", "Item", new { Id = model.SubCategoryId });
               
            }
            catch
            {
                return View(model);
            }
        }
        #endregion


    }
}
