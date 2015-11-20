using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebshopAdmin;
using System.Windows.Media.Imaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        WebshopAdmin.Service.Service service = new WebshopAdmin.Service.Service();
        WebshopAdmin.lewebshopEntities1 db = WebshopAdmin.Dao.Database.db;

        [TestInitialize]
        public void TestInit()
        {

        }

        [TestMethod]
        public void TestCreateProduct()
        {
            service.createProduct("Test ost", (decimal)20, 20, @"C:\Users\Jens\Source\Repos\LeWebShop2\WebshopAdmin\Webshop\pic\ost3.jpg", "Danmark", "Test kategori", "MMMMMM OSTE",true);

            var queryProduct = from product in db.Products
                               where product.name == "Test Ost"
                               select product;

            //BitmapImage img = service.getImage(@"C:\Users\Jens\Source\Repos\LeWebShop2\WebshopAdmin\Webshop\pic\ost3.jpg");

            WebshopAdmin.Product p = queryProduct.ToList().ElementAt(1);
            Assert.AreEqual("Test ost", p.name);
            Assert.AreEqual((decimal)20, p.unitPrice);
            Assert.AreEqual(20, p.countAvailable);
            //CollectionAssert.AreEqual(service.convertToByteArray(img), p.picture);
            Assert.AreEqual((decimal)0, p.rating);
            Assert.AreEqual("Danmark", p.country);
            Assert.AreEqual("Test kategori", p.category);
            Assert.AreEqual("MMMMMM OSTE", p.description);
            Assert.AreEqual(true, p.@new);
        }

        [TestMethod]

        public void TestCreatePackage()
        {
            List <Product> l = new List<Product>();
            l = db.Products.ToList();
            service.createPackage(l, "Danske oste", 200);
           
                
            var queryPackage = from package in db.Packages
                               where package.name == "Danske oste"
                               select package;
    
            
            Package pac = queryPackage.ToList().ElementAt(0);
            Assert.AreEqual("Danske oste", pac.name);
            Assert.AreEqual(200, pac.price);

        }

        [TestMethod]

        public void TestCreateFAQ()
        {         
            service.createFAQ("LUGTER OSTEN?", "JA");
            
             var queryFAQ = from faq in db.FAQs
                               where faq.question == "LUGTER OSTEN?"
                               select faq;

            FAQ faq1 =  queryFAQ.ToList().ElementAt(0);

            Assert.AreEqual("LUGTER OSTEN?", faq1.question);
            Assert.AreEqual("JA", faq1.answer);        
        }
        [TestMethod]
        public void TestStats()
        {
            int i = service.numberOfSales(DateTime.Today, DateTime.Today.AddDays(1));
            Assert.AreEqual(0, i);
        }
    }
}
