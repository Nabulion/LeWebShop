//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebshopAdmin
{
    using System;
    using System.Collections.Generic;
    
    public partial class OrderLine
    {
        public int id { get; set; }
        public Nullable<int> productCount { get; set; }
        public Nullable<int> Product { get; set; }
        public Nullable<int> ShoppingCart { get; set; }
    
        public virtual Product Product1 { get; set; }
        public virtual ShoppingCart ShoppingCart1 { get; set; }
    }
}
