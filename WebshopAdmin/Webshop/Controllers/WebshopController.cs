using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Webshop.Controllers
{
    public class WebshopController : Controller
    {
        // GET: Webshop
        public ActionResult Index(int? id)
        {
            UserProfile temp = Dao.Dao.getDB().UserProfiles.Find(id);
            if (temp == null)
            {
                temp = new UserProfile();
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
            return View(new Visa());
        }
        [HttpPost]
        public ActionResult visa(FormCollection fc, int id)
        {
            String visa = fc["visa"];
            UserProfile temp = Service.Service.findUser(id);
            Service.Service.createVisa(temp, visa);
            return RedirectToAction("userProfile", new { id = temp.id });
        }

    }
}