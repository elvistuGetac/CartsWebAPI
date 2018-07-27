using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Carts.Controllers
{
	public class OrderController : Controller
	{
		// GET: Order
		public ActionResult Index()
		{
			var userId = HttpContext.User.Identity.GetUserId();

			if (userId != null)
			{
				ViewBag.LinkableId = 666;
				return View();
			}
			else
			{
				return RedirectToAction("Login", "Account");
			}
		}
	}
}