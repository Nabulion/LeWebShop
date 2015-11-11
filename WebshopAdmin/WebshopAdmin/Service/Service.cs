using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopAdmin;

namespace webshopAdmin.Service
{
    public class Service
    {
        lewebshopEntities db = Dao.Database.db;
        public void createProduct(string name, decimal unitprice, int countavailable, string pic, decimal rating, string country)
        {
            Product p = new Product();
            p.name = name;
            // Skal have path til billede
            p.pic = pic;
            p.unitPrice = unitprice;
            p.rating = 0;
            p.countAvailable = countavailable;
            p.country = country;
            db.Products.Add(p);
            db.SaveChanges();

        }
        public void deleteProduct(Product p)
        {

            if (db.Products.Contains(p) && p != null)
            {
                db.Products.Remove(p);
            }
        }
        public void updateProduct(Product p, string newName, decimal newUnitprice, int newCountavailable, string newPic, decimal newRating, string newCountry)
        {
            if (newName != null && newUnitprice != null && p != null && newCountavailable != null && newPic != null && newRating != null)
            {
                p.countAvailable = newCountavailable;
                p.name = newName;
                p.country = newCountry;
                p.unitPrice = newUnitprice;
                p.countAvailable = newCountavailable;
                p.pic = newPic;
                p.rating = newRating;
                // Listbox skal opdateres
                db.SaveChanges();
            }
        }


    }
}
