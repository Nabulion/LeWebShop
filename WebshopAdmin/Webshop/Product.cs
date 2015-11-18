//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Webshop
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Comments = new HashSet<Comment>();
            this.OrderLines = new HashSet<OrderLine>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<decimal> unitPrice { get; set; }
        public Nullable<int> countAvailable { get; set; }
        public string country { get; set; }
        public Nullable<decimal> rating { get; set; }
        public byte[] picture { get; set; }
        public string category { get; set; }
        public string description { get; set; }
        public Nullable<bool> @new { get; set; }
        public Nullable<int> Package { get; set; }
        public Nullable<int> ShoppingCart { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderLine> OrderLines { get; set; }
        public virtual Package Package1 { get; set; }
        public virtual ShoppingCart ShoppingCart1 { get; set; }
    }
}
