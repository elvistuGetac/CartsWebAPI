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
	public class ProductInfoController : ApiController
    {
		private CartsServerEntities db = new CartsServerEntities();

		public struct ProduceInfo
		{
			public int Id;
			public string Name;
			public string Manufacturer;
			public string Brand;
			public string Description;
			public int Price;
			public string ImageUri;
		}

		[Route("api/ProductInfos")]
		public ProduceInfo[] GetProductInfos()
		{
			ProduceInfo[] ProduceInfos= new ProduceInfo[db.Products.Count()];
			List<Models.Product> products = db.Products.ToList();

			for (int i = 0; i < ProduceInfos.Length; i++)
			{
				int produceId = products[i].Id;
				ProduceInfos[i].Id = produceId;
				ProduceInfos[i].Name = products[i].Name;
				ProduceInfos[i].Brand = products[i].Brand;
				ProduceInfos[i].Description = products[i].Description;
				ProduceInfos[i].Price = db.ProductPrices.Where(o => o.ProductId_Id == produceId).FirstOrDefault().Price;
				ProduceInfos[i].ImageUri = db.ProductAssets.Where(o => o.ProductId_Id == produceId).FirstOrDefault().ImageUri;
			}

			return ProduceInfos;
		}

		[Route("api/ProductInfos/{id:int}")]
		public ProduceInfo GetProductInfo(int id)
		{
			ProduceInfo ProduceInfo = new ProduceInfo();
			Product product = db.Products.Where(p => p.Id == id).FirstOrDefault();

			ProduceInfo.Id = id;
			ProduceInfo.Name = product.Name;
			ProduceInfo.Manufacturer = product.Manufacturer;
			ProduceInfo.Brand = product.Brand;
			ProduceInfo.Description = product.Description;
			ProduceInfo.Price = db.ProductPrices.Where(o => o.ProductId_Id == id).FirstOrDefault().Price;
			ProduceInfo.ImageUri = db.ProductAssets.Where(o => o.ProductId_Id == id).FirstOrDefault().ImageUri;

			return ProduceInfo;
		}
		
		[Route("api/ProductInfos/add")]
		public void PostProductInfo(ProduceInfo produceInfo)
		{
			Product product = new Product();
			ProductPrice productPrice = new ProductPrice();
			ProductAsset productAsset = new ProductAsset();

			product.Name = produceInfo.Name;
			product.Manufacturer = produceInfo.Manufacturer;
			product.Brand = produceInfo.Brand;
			product.Description = produceInfo.Description;
			product.ProductCode = new Random().Next(65535);
			product.Status = "Y";
			product.LastUpdated = DateTime.Now;

			db.Products.Add(product);
			db.SaveChanges();

			int LastProductId = db.Products.OrderByDescending(p => p.Id).FirstOrDefault().Id;
			productPrice.Price = produceInfo.Price;
			productPrice.EffectiveFrom = DateTime.Now.ToShortDateString();
			productPrice.LastUpdated = DateTime.Now;
			productPrice.ProductId_Id = LastProductId;

			productAsset.ImageUri = produceInfo.ImageUri;
			productAsset.Size = "100x100";
			productAsset.Type = "Front";
			productAsset.LastUpdated = DateTime.Now;
			productAsset.ProductId_Id = LastProductId;

			db.ProductPrices.Add(productPrice);
			db.ProductAssets.Add(productAsset);
			db.SaveChanges();
		}

		[Route("api/ProductInfos/edit/{id:int}")]
		public void PutProductInfo(int id, ProduceInfo produceInfo)
		{
			Product product = db.Products.Find(id);
			ProductPrice productPrice = db.ProductPrices.Where(o => o.ProductId_Id == id).FirstOrDefault();
			ProductAsset productAsset = db.ProductAssets.Where(o => o.ProductId_Id == id).FirstOrDefault();

			product.Name = produceInfo.Name;
			product.Manufacturer = produceInfo.Manufacturer;
			product.Brand = produceInfo.Brand;
			product.Description = produceInfo.Description;
			product.LastUpdated = DateTime.Now;

			productPrice.Price = produceInfo.Price;
			productPrice.LastUpdated = DateTime.Now;

			productAsset.ImageUri = produceInfo.ImageUri;
			productAsset.LastUpdated = DateTime.Now;

			db.Entry(product).State = EntityState.Modified;
			db.Entry(productPrice).State = EntityState.Modified;
			db.Entry(productAsset).State = EntityState.Modified;
			db.SaveChanges();
		}
	}
}
