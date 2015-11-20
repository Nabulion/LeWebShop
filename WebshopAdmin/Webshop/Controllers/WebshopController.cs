using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class WebshopController : Controller
    {
        // GET: Webshop
        public ActionResult Index(int? id)
        {

            UserProfile temp = Dao.Dao.getDB().UserProfiles.Find(id.GetValueOrDefault());
            if (temp == null)
            {
                temp = Service.Service.getDefaultUser();
            }
            return View(temp);
        }
        public ActionResult userProfile(int id)
        {
            UserProfile temp = Service.Service.findUser(id);
            return View(temp);
        }
        public ActionResult updateUser(int id)
        {
            UserProfile temp = Service.Service.findUser(id);
            return View(temp);
        }
        [HttpPost]
        public ActionResult updateUser(FormCollection fc, int id)
        {
            UserProfile temp = Service.Service.findUser(id);
            bool newsl = fc["newsletter"].Contains("true");
            Service.Service.updateUserLogin(temp, fc["name"], (fc["pass"]), fc["Email"], fc["adress"], fc["zipcode"], newsl);
            return RedirectToAction("userProfile", new { id = temp.id });
        }
        public ActionResult visa(int id)
        {
            UserProfile temp = Service.Service.findUser(id);
            Visa v = new Visa();
            v.UserProfile1 = temp;
            return View(v);
        }
        [HttpPost]
        public ActionResult visa(FormCollection fc, int id)
        {
            String visa = fc["visa"];
            UserProfile temp = Service.Service.findUser(id);
            Service.Service.createVisa(temp, visa);
            return RedirectToAction("userProfile", new { id = temp.id });
        }
        public ActionResult DiscountCheese(int? id)
        {
            List<Product> list = Service.Service.getProductCategory("Discount pris");
            Wrapper w = new Wrapper();
            w.list = list;
            w.userprofile = Service.Service.findUser(id.GetValueOrDefault());
            return View(w);
        }
        public ActionResult EveryDayCheese(int? id)
        {
            List<Product> list = Service.Service.getProductCategory("Hverdags oste");
            Wrapper w = new Wrapper();
            w.list = list;
            w.userprofile = Service.Service.findUser(id.GetValueOrDefault());
            return View(w);
        }
        public ActionResult luxusCheese(int? id)
        {
            List<Product> list = Service.Service.getProductCategory("Luxus oste");
            Wrapper w = new Wrapper();
            w.list = list;
            w.userprofile = Service.Service.findUser(id.GetValueOrDefault());
            return View(w);
        }
        public ActionResult exclusiveCheese(int? id)
        {
            List<Product> list = Service.Service.getProductCategory("Eksklusive oste");
            Wrapper w = new Wrapper();
            w.list = list;
            w.userprofile = Service.Service.findUser(id.GetValueOrDefault());
            return View(w);
        }
        public ActionResult pieceCheese(int? id)
        {
            List<Product> list = Service.Service.getProductCategory("Styk ost");
            Wrapper w = new Wrapper();
            w.list = list;
            w.userprofile = Service.Service.findUser(id.GetValueOrDefault());
            return View(w);
        }
        public ActionResult cheeseTable(int? id)
        {
            List<Product> list = Service.Service.getProductCategory("Osteborde");
            Wrapper w = new Wrapper();
            w.list = list;
            w.userprofile = Service.Service.findUser(id.GetValueOrDefault());
            return View(w);
        }
        public ActionResult productInfo(int id, int? bid)
        {
            Wrapper w = new Wrapper();
            w.userprofile = Service.Service.findUser(bid.GetValueOrDefault());
            w.produkt = Service.Service.findProduct(id);
            return View(w);
        }
        [HttpPost]
        public ActionResult productInfo(FormCollection fc, int id, int bid)
        {
            int Antal = int.Parse(fc["Antal"]);
            UserProfile u = Service.Service.findUser(bid);
            Product p = Service.Service.findProduct(id);
            Service.Service.addToCart(u, p, Antal);
            return RedirectToAction("productInfo", new { id = p.id, bid = u.id });
        }
        public ActionResult IndkoebsKurv()
        {

        }
    }
}