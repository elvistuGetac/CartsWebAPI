using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carts.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

		public ActionResult Create()
		{
			return View();
		}

		public ActionResult Edit(string id)
		{
			try
			{
				ViewBag.LinkableId = Int32.Parse(id);
				return View();
			}
			catch (Exception ex)
			{
				return RedirectToAction("Index");
			}
		}
	}
}