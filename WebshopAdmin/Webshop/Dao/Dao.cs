
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Dao
{
    public class Dao
    {
        private static lewebshopEntities4 DB;

        public static lewebshopEntities4 getDB()
        {
            if (DB == null)
            {
                DB = new lewebshopEntities4();
            }
            return DB;
        }
        public static UserProfile findUser(String pass, String name)
        {
            UserProfile temp = (from l in DB.UserProfiles where l.LoginUser.pass == pass && l.LoginUser.name == name select l).First();
            return temp;
        }
        public static void updateUser(UserProfile u, String newname, String newpass, String newEmail, String newAdress, String zipCode, bool newsletter)
        {

            u.LoginUser.name = newname;
            u.LoginUser.pass = newpass;
            u.Email = newEmail;
            u.adress = newAdress;
            u.zipcode = zipCode;
            u.newsletter = newsletter;
            DB.SaveChanges();
        }
        public static Visa visaInfo(UserProfile temp, String Visa)
        {
            Visa v = new Visa();
            v.UserProfile1 = temp;
            v.visainfo = Visa;
            DB.Visas.Add(v);
            DB.SaveChanges();
            return v;
        }
        public static List<Product> getProduct(String category)
        {

            var product = from p in DB.Products where p.category == category select p;
            
            return product.ToList();
        }
        public static Product findProduct(int id)
        {
            return DB.Products.Find(id);
        }
        public static ShoppingCart createShoppingCart(UserProfile u)
        {
            ShoppingCart sc = new ShoppingCart();
            sc.ShoppingOrders = new List<ShoppingOrder>();
            sc.UserProfile1 = u;
            DB.ShoppingCarts.Add(sc);
            DB.SaveChanges();
            return sc;
        }
        public static void AddToCart(UserProfile u, Product p, int count)
        {
            OrderLine ol = new OrderLine();
            ol.Product = p.id;
            ol.productCount = count;
            ol.ShoppingCart1 = u.ShoppingCarts.FirstOrDefault();
            DB.OrderLines.Add(ol);
            DB.SaveChanges();
        }
    }
}