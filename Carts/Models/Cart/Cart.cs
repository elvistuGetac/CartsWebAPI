using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carts.Models.Cart
{
    [Serializable]
    public class Cart : IEnumerable<CartItem>
    {
        public Cart()
        {
            this.cartItems = new List<CartItem>();
        }

        private List<CartItem> cartItems;

        public int Count
        {
            get
            {
                return this.cartItems.Count;
            }
        }

        public decimal TotalAmount
        {
            get 
            {
                decimal totalAmount = 0.0m;
                foreach(var cartItem in this.cartItems)
                {
                    totalAmount = totalAmount + cartItem.Amount;
                }
                return totalAmount;
            }
        }

        public bool AddProduct(int ProductId)
        {
            var findItem = this.cartItems
                            .Where(s => s.Id == ProductId)
                            .Select(s => s)
                            .FirstOrDefault();

            if (findItem == default(Models.Cart.CartItem))
            { 
                using( Models.CartsServerEntities db = new CartsServerEntities() )
                {
                    var product = (from s in db.Products 
                                  where s.Id == ProductId 
                                  select s).FirstOrDefault();
                    if( product != default( Models.Product ) )
                    {
                        this.AddProduct(product);
                    }
                }             
            }
            else
            {
                findItem.Quantity += 1;
            }
            return true;
        }

        private bool AddProduct(Product product)
        {
            var cartItem = new Models.Cart.CartItem()
            {
				Id = product.Id,
				Name = product.Name,
				Price = product.ProductPrices.ToList()[0].Price,
				DefaultImageURL = product.ProductAssets.ToList()[0].ImageUri,
				Quantity = 1
			};

            //加入CartItem至購物車
            this.cartItems.Add(cartItem);
            return true;
        }

		//移除一筆Product，使用ProductId
		public bool RemoveProduct(int ProductId)
		{
			var findItem = this.cartItems
							.Where(s => s.Id == ProductId)
							.Select(s => s)
							.FirstOrDefault();

			if (findItem == default(Models.Cart.CartItem))
			{

			}
			else
			{
				this.cartItems.Remove(findItem);
			}
			return true;
		}

		public bool ClearCart()
		{
			this.cartItems.Clear();
			return true;
		}

		#region IEnumerator

		IEnumerator<CartItem> IEnumerable<CartItem>.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.cartItems.GetEnumerator();
        }

        #endregion
    }
}


