
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Webshop.Dao
{
    public class Dao
    {
        private static lewebshopEntities1 DB;

        public static lewebshopEntities1 getDB()
        {
            if (DB == null)
            {
                DB = new lewebshopEntities1();
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
            //new List<Product>();
             
            /*for (int i = 0; i < DB.Products.Count(); i++) {
                if (DB.Products.ToList()[i].category == category)
                {
                    product.Add(DB.Products.ToList()[i]);
                }
            }*/
            return product.ToList();
        }
        public static Product findProduct(int id)
        {
            return DB.Products.Find(id);
        }
    }
}