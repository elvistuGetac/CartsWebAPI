//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Carts.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            this.OrderLines = new HashSet<OrderLine>();
        }
    
        public int Id { get; set; }
        public int OrderNum { get; set; }
        public string Description { get; set; }
        public string CurrencyType { get; set; }
        public string Status { get; set; }
        public int TotalAmount { get; set; }
        public string CreatedOn { get; set; }
        public System.DateTime LastUpdated { get; set; }
        public int UserId_Id { get; set; }
        public int ShippingAddress_Id { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual User User { get; set; }
        public virtual UserAddress UserAddress { get; set; }
    }
}
