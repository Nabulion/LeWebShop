//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UnitTestProject1
{
    using System;
    using System.Collections.Generic;
    
    public partial class Comment
    {
        public int id { get; set; }
        public string comment1 { get; set; }
        public Nullable<int> Product { get; set; }
        public Nullable<int> UserProfile { get; set; }
    
        public virtual Product Product1 { get; set; }
        public virtual UserProfile UserProfile1 { get; set; }
    }
}
