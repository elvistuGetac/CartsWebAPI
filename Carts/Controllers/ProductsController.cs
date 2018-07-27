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
    public class ProductsController : ApiController
    {
        private CartsServerEntities db = new CartsServerEntities();

        // GET: api/Products
        public IQueryable<Product> GetProducts()
        {
            return db.Products;
        }

        // GET: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult GetProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // PUT: api/Products/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product.Id)
            {
                return BadRequest();
            }

            db.Entry(product).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Products
        [ResponseType(typeof(Product))]
        public IHttpActionResult PostProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(product);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [ResponseType(typeof(Product))]
        public IHttpActionResult DeleteProduct(int id)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }

			List<Models.ProductPrice> ProductPrices = db.ProductPrices.Where(o => o.ProductId_Id == id).ToList();
			foreach (var p in ProductPrices) {
				db.ProductPrices.Remove(p);
			}
			List<Models.ProductAsset> ProductAssets = db.ProductAssets.Where(o => o.ProductId_Id == id).ToList();
			foreach (var p in ProductAssets)
			{
				db.ProductAssets.Remove(p);
			}
			List<Models.ProductAttribute> ProductAttributes = db.ProductAttributes.Where(o => o.ProductId_Id == id).ToList();
			foreach (var p in ProductAttributes)
			{
				db.ProductAttributes.Remove(p);
			}
			List<Models.ProductCategory> ProductCategories = db.ProductCategories.Where(o => o.ProductId_Id == id).ToList();
			foreach (var p in ProductCategories)
			{
				db.ProductCategories.Remove(p);
			}

			db.Products.Remove(product);
            db.SaveChanges();

            return Ok(product);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}