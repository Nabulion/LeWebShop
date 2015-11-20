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
                temp = new UserProfile();
                temp.id = 0;
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
        public ActionResult DiscountCheese(int id)
        {
            List<Product> list = Service.Service.getProductCategory("Discount pris");
            Wrapper w = new Wrapper();
            w.list = list;
            w.userprofile = new UserProfile();
            if (id == 0)
            {
                w.userprofile.id = 0;
            }
            else
            {
                w.userprofile = Service.Service.findUser(id);
            }
            return View(w);
        }
        public ActionResult productInfo(int id)
        {
            Wrapper w = new Wrapper();
            w.produkt = Service.Service.findProduct(id);
            return View(w);
        }

    }
}