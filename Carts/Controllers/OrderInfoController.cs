using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Carts.Models;

namespace Carts.Controllers
{
    public class OrderInfoController : ApiController
    {
		private CartsServerEntities db = new CartsServerEntities();

		public struct OrderInfo
		{
			public int UserId;
			public string ShippingAddress_Id;
			public int TotalAmount;
		}

		[Route("api/Order/add")]
		public void PostOrder(OrderInfo orderInfo)
		{
			Order order = new Order();

			//order.UserId_Id = orderInfo.UserId;
			order.ShippingAddress_Id = 0;
			order.TotalAmount = orderInfo.TotalAmount;
			order.LastUpdated = DateTime.Now;
			order.Status = "Y";
			order.CreatedOn = "Y";
			order.OrderNum = new Random().Next();

			db.Orders.Add(order);
			db.SaveChanges();

			return;
		}
	}
}
