using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using OnlineShopping.BL.Interface;
using OnlineShopping.BL.Model;
using OnlineShopping.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net;
using NuGet.Protocol;
using System.Timers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.AspNetCore.Identity;
using OnlineShopping.DAL.Extend;
using Microsoft.CodeAnalysis.Options;

namespace OnlineShopping.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        #region Cons
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly UserManager<ApplicationUser> userManager;

        public HomeController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser>userManager)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.userManager = userManager;
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
            var data = unitOfWork.SubCategory.GetAll(a => a.CategoryId == Id);
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
        [HttpPost]
        public IActionResult Details(ItemVM model)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(100);

            if (HttpContext.Request.Cookies["cart"] == null)
            {
                List<CartVM> test = new List<CartVM>();
                var obj = unitOfWork.Item.GetById(a => a.Id == model.Id);
                test.Add(new CartVM { Item = obj, Quantity = model.Quantity });
                HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(test), option);
            }
            else
            {
                List<CartVM> test = JsonConvert.DeserializeObject<List<CartVM>>(HttpContext.Request.Cookies["cart"]);
                if (test == null)
                {
                    test = new List<CartVM>();
                }
                else 
                {
                    foreach (var item in test)
                    {
                        if (item.Item.Id == model.Id)
                        {
                            model.Quantity += item.Quantity;
                            test.Remove(item);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                var obj = unitOfWork.Item.GetById(a => a.Id == model.Id);
                test.Add(new CartVM { Item = obj, Quantity = model.Quantity });
                HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(test), option);

               
            }

            return RedirectToAction("Item", new { Id = model.SubCategoryId });
        }
        public IActionResult Cart()
        {
            try
            {
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(100);
                if (HttpContext.Request.Cookies["cart"] != null)
                {
                    List<CartVM> test = JsonConvert.DeserializeObject<List<CartVM>>(HttpContext.Request.Cookies["cart"]);

                    if (test != null)
                    {
                        foreach (var item in test)
                        {
                            item.Item = unitOfWork.Item.GetById(a => a.Id == item.Item.Id);

                        }
                    }
                    HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(test), option);
                    return View(test);
                }
                else
                {
                    List<CartVM> test = null;
                    return View(test);
                }
            }
            catch
            {
                List<CartVM> test = null;
                return View(test);
            }

        }
        public IActionResult DeleteItem(int Id)
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(100);
            List<CartVM> test = JsonConvert.DeserializeObject<List<CartVM>>(HttpContext.Request.Cookies["cart"]);
            var obj = unitOfWork.Item.GetById(a => a.Id == Id);
            foreach (var item in test)
            {
                if (item.Item.Id == obj.Id)
                {
                    test.Remove(item);
                    break;
                }

            }
            HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(test), option);
            return RedirectToAction("Cart");
        }


        public async Task< IActionResult> Order()
        
        {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(100);
            List<CartVM>? test = JsonConvert.DeserializeObject<List<CartVM>>(HttpContext.Request.Cookies["cart"]);
            if (test == null)
            {
                return RedirectToAction("Cart");
            }
            else
            {
                
                foreach (var item in test)
                {
                    item.Item = unitOfWork.Item.GetById(a => a.Id == item.Item.Id);

                }
                HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(test), option);
                var user = userManager.GetUserAsync(HttpContext.User).Result.Id;
                var order = unitOfWork.Order.AddWithReturn(new Order { ApplicationUserId = user });
                
                var orderDetailsList = new List<OrderDetails>();
                foreach (var item in test)
                {
                    if (item.Item.Count == 0)
                    {
                        test.Remove(item);
                        HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(test), option);
                        TempData["mes"] = item.Item.Name + "Not Available Now";
                        return RedirectToAction("Cart");
                    }
                    else if (item.Quantity > item.Item.Count)
                    {
                        item.Quantity = item.Item.Count;
                    }
                    var orderDetails = new OrderDetails
                    {
                        ItemId = item.Item.Id,
                        Quantity = item.Quantity,
                        TotalPrice = Convert.ToDouble(item.Quantity) * (item.Item.price - item.Item.discount),
                        OrderId = order.Id
                    };
                    var itemObj = unitOfWork.Item.GetById(a => a.Id == item.Item.Id);
                    itemObj.Count = itemObj.Count - item.Quantity;
                    unitOfWork.Item.Update(itemObj);
                    unitOfWork.Complete();
                    orderDetailsList.Add(orderDetails);                    
                }
                unitOfWork.OrderDetails.AddRange(orderDetailsList);
                HttpContext.Response.Cookies.Delete("cart");
                return RedirectToAction("Cart");
            }
        }


    }
}
