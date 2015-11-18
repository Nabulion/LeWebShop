using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Models
{
    public class Wrapper
    {
        public UserProfile userprofile { get; set; }
        public LoginUser loginuser { get; set; }
        public Product produkt { get; set; }
        public List<Product> list { get; set; }
    }
}