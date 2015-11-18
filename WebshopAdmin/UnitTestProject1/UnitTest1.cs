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
        WebshopAdmin.lewebshopEntities db = WebshopAdmin.Dao.Database.db;

        [TestInitialize]
        public void TestInit()
        {

        }

        [TestMethod]
        public void TestCreateProduct()
        {
            service.createProduct("Test Ost", (decimal)20, 100, @"C:\Users\Jens\Source\Repos\LeWebShop2\WebshopAdmin\Webshop\pic\ost3.jpg", (decimal)5, "Danmark", "Test Katagori", true);
            var queryProduct = from product in db.Products
                               where product.name == "Test Ost"
                               select product;    
 
            //BitmapImage img = service.getImage(@"C:\Users\Jens\Source\Repos\LeWebShop2\WebshopAdmin\Webshop\pic\ost3.jpg");

            WebshopAdmin.Product p = queryProduct.ToList().ElementAt(0);
            Assert.AreEqual("Test Ost", p.name);
            Assert.AreEqual((decimal)20, p.unitPrice);
            Assert.AreEqual(100, p.countAvailable);
            //Assert.AreEqual(service.convertToByteArray(img), p.picture);
            Assert.AreEqual((decimal)5, p.rating);
            Assert.AreEqual("Danmark", p.country);
            Assert.AreEqual("Test Katagori", p.category);
            Assert.AreEqual(true, p.@new);
        }
    }
}
