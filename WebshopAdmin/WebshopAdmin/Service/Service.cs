﻿using System;
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
using System.Net.Mail;


namespace WebshopAdmin.Service
{
    public class Service
    {
        lewebshopEntities1 db = Dao.Database.db;
        
        DataTable dt;

        public Service()
        {
            dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("unitprice");
            dt.Columns.Add("countavailable");
            dt.Columns.Add("country");
            dt.Columns.Add("pic");
            dt.Columns.Add("description");
            dt.Columns.Add("rating");
            
        }

        // Product

        public Product createProduct(string name, decimal unitprice, int countavailable, string pic, string country, string category, string description, Boolean newP)
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
            p.description = description;
            p.@new = newP;
            p.unitPrice = unitprice;
            p.rating = 0;
            p.countAvailable = countavailable;
            p.country = country;
            p.monthsale = false;
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
            if (p != null)
            {
                db.Products.Remove(p);
                db.SaveChanges();
            }
        }
        public void updateProduct(Product p, string newName, decimal newUnitprice, int newCountavailable, string newCountry, string newPic, string description, string category, Boolean @new)
        {
            if (newName != null && p != null && newPic != null)
            {
                p.name = newName;
                p.unitPrice = newUnitprice;
                p.countAvailable = newCountavailable;
                p.country = newCountry;
                try { 
                p.picture = convertToByteArray(new BitmapImage(new Uri(@newPic)));
                }
                catch
                {
                    p.picture = null;
                } 
                p.description = description;
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

        public List<Product> getSaleProduct()
        {
            var querySaleProduct = from product in db.Products
                                   where product.monthsale == true
                                   select product;
            return querySaleProduct.ToList();
        }

        public List<Package> getPackages()
        {
            List<Package> p = new List<Package>();
            p = db.Packages.ToList();
            return p;
        }

        public List<Package> getSalePackage()
        {
            var querySalePackage = from package in db.Packages
                                   where package.monthsale == true
                                   select package;
            return querySalePackage.ToList();
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
                row["country"] = product.country;
                row["rating"] = Convert.ToString(product.rating);
                row["description"] = product.description;
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
            p.monthsale = false;
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
                    if (imageByteArray != null)
                    {
                        MemoryStream ms = new MemoryStream(imageByteArray);
                        biImg.BeginInit();
                        biImg.StreamSource = ms;
                        biImg.EndInit();

                        BitmapImage imgSrc = biImg;
                    }
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


        //Mail
        
        public void sendNewsLetter(string content, string subject)
        {
            MailMessage m = new MailMessage();
            m.From = new MailAddress("me@mycompany.com");
            List<UserProfile> users = new List<UserProfile>();
            users = db.UserProfiles.ToList();
            foreach (UserProfile u in users)
            {
                m.To.Add(u.Email);
            }
            
            m.Subject = subject;
            m.Body = content;

            SmtpClient smtp = new SmtpClient("127.0.0.1");
            smtp.Send(m);
        }

        public int numberOfSales(DateTime start, DateTime end)
        {
            int i = 0;
            List<ShoppingOrder> l = new List<ShoppingOrder>();
            l = db.ShoppingOrders.ToList();
            if (l.Count != 0 && start != null && end != null)
            {
                foreach (ShoppingOrder s in l)
                {
                    if (s.dato > start && s.dato < end)
                    {
                        i++;
                    }
                }
            }
            return i;

        }
        public decimal averagePriceOnSales(DateTime start, DateTime end)
        {
            decimal d = 0;
            List<ShoppingOrder> l = new List<ShoppingOrder>();
            l = db.ShoppingOrders.ToList();
            if (l.Count != 0 && start != null && end != null)
            {
                foreach (ShoppingOrder s in l)
                {
                    d = (decimal)s.orderPrice + d;
                }
                return d/l.Count;
            }
            return d;
        }
        

    }
}
