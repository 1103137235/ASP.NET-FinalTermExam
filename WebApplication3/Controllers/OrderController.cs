using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication3.Controllers
{
    public class OrderController : Controller
    {
        Models.OrderService orderService = new Models.OrderService();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        public ActionResult Index(Models.OrderSearchArg arg)
        {
            if (arg.Delete != null)
            {
                orderService.DeleteOrderById(arg.Delete);
            }
            else if (arg.Insert != null)
            {
                ModelState.Clear();
                return View("InsertOrder", new Models.Order());
            }
            ViewBag.SearchResult = orderService.GetOrderByCondtioin(arg);
            return View("Index");
        }

        [HttpPost()]
        public ActionResult InsertOrder(Models.Order order)
        {
            orderService.InsertOrder(order);
            ModelState.Clear();
            return View("index");
        }
    }
}