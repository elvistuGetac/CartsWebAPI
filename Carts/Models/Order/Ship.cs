using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Carts.Models.OrderModel
{
	public class Ship
	{
		[Required]
		[Display(Name = "Reciever Name")]
		[StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long."
			, MinimumLength = 2)]
		public string RecieverName { get; set; }

		[Required]
		[Display(Name = "Reciever Phone")]
		[StringLength(15, ErrorMessage = "The {0} must be at least {2} characters long."
			, MinimumLength = 8)] 
		public string RecieverPhone { get; set; }

		[Required]
		[Display(Name = "Reciever Address")]
		[StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long."
			, MinimumLength = 8)]
		public string RecieverAddress { get; set; }
	}
}