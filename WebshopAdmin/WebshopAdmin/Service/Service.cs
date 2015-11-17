using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopAdmin;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows.Controls;


namespace WebshopAdmin.Service
{
    public class Service
    {
        lewebshopEntities db = Dao.Database.db;
        DataTable dt;

        public Service()
        {
            dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("unitprice");
            dt.Columns.Add("countavailable");
            dt.Columns.Add("pic");
            dt.Columns.Add("country");
            dt.Columns.Add("rating");
           
            
        }

        // Product

        public Product createProduct(string name, decimal unitprice, int countavailable, string pic, decimal rating, string country, string category, Boolean newP)
        {
            Product p = new Product();
            p.name = name;
            try
            {
                p.picture=convertToByteArray(new BitmapImage(new Uri(@pic)));
            }
            catch
            {
                p.picture = null;
            }
            p.category = category;
            p.@new = newP;
            p.unitPrice = unitprice;
            p.rating = 0;
            p.countAvailable = countavailable;
            p.country = country;
            db.Products.Add(p);
            db.SaveChanges();

            return p;

        }
     

        public Boolean findPicture(string s, Image i)
        {
            try
            {
                BitmapImage bi = new BitmapImage(new Uri(@s));
                i.Source = bi;
                return true;
            }
            catch
            {
                return false;
            }

        }
        public void deleteProduct(Product p)
        {
                db.Products.Remove(p);
                db.SaveChanges();
        }
        public void updateProduct(Product p, string newName, decimal newUnitprice, int newCountavailable, string newPic, decimal newRating, string newCountry, string category, Boolean @new)
        {
            if (newName != null && p != null && newPic != null)
            {
                p.countAvailable = newCountavailable;
                p.name = newName;
                p.country = newCountry;
                p.unitPrice = newUnitprice;
                p.countAvailable = newCountavailable;
                p.picture = convertToByteArray(new BitmapImage(new Uri(@newPic)));
                p.rating = newRating;
                p.category = category;
                p.@new = @new;
                db.SaveChanges();
                filldata(db.Products.ToList());
            }
        }
        public List<Product> getProducts()
        {
            List<Product> l = new List<Product>();
            l = db.Products.ToList();
            return l;
        }
        public DataTable getTable()
        {
            return dt;
        }
        public DataTable filldata(List<Product> l)
        {
            foreach (var product in l)
            {
                var row = dt.NewRow();
                row["name"] = product.name;
                row["unitprice"] = Convert.ToString(product.unitPrice);
                row["countavailable"] = Convert.ToString(product.countAvailable);
                //row["pic"] = product.pic;
                row["country"] = product.country;
                row["rating"] = Convert.ToString(product.rating);
                dt.Rows.Add(row);
            }

            return dt;
        }


        // FAQ 
        public void createFAQ(string question, string answer)
        {
            FAQ f = new FAQ();
            f.answer = answer;
            f.question = question;
            db.FAQs.Add(f);
            db.SaveChanges();

        }
        public void deleteFAQ(FAQ faq)
        {
            db.FAQs.Remove(faq);
            db.SaveChanges();
        }
        public void editFAQ(FAQ faq, string question, string answer)
        {
            faq.question = question;
            faq.answer = answer;
            db.SaveChanges();
            
        }


        // Package

        public void createPackage(List<Product> list, string name, decimal price)
        {
            Package p = new Package();
            p.name = name;
            p.price = price;
            foreach (Product pro in list)
            {
                pro.Package1 = p;
            }
            db.Packages.Add(p);
            db.SaveChanges();
        }
        public void deletePackage(Package p)
        {

            db.Packages.Remove(p);
            db.SaveChanges();
        }
        public void editpackage(List<Product> l,Package p, string name, decimal price)
        {

            p.name = name;
            p.price = price;
            foreach (Product pro in l)
            {
                pro.Package1 = p;
            }

            db.SaveChanges();

        }
        // Get picture from database
        public BitmapImage getImage(string s)
        {
            List<Product> l = new List<Product>();
            l = db.Products.ToList();
            BitmapImage biImg = new BitmapImage();
            int i = 0;
            foreach (Product p in l)
            {
                i++;
                if (p.name == s)
                {
                    Byte[] imageByteArray = db.Products.ToList().ElementAt(i-1).picture;
                    MemoryStream ms = new MemoryStream(imageByteArray);
                    biImg.BeginInit();
                    biImg.StreamSource = ms;
                    biImg.EndInit();

                    BitmapImage imgSrc = biImg;
                }
            }
            
                return biImg;
          
        }
        // Writes picture to database
        public byte[] convertToByteArray(BitmapImage img)
        {
            MemoryStream ms = new MemoryStream();
            JpegBitmapEncoder enconder = new JpegBitmapEncoder();
            enconder.Frames.Add(BitmapFrame.Create(img));
            enconder.Save(ms);
            return ms.ToArray();
        }

    }
}
