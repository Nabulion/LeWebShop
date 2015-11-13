using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webshop.Models;

namespace Webshop.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            String pass = fc["password"];
            try {
                LoginUser temp = Service.Service.validateLogin(pass);
                return RedirectToAction("userProfile", temp);
            }
            catch
            {
                return View("");
            }

            
        }
        public ActionResult createUser()
        {
            Wrapper w = new Wrapper();
            w.loginuser = new LoginUser();
            w.userprofile = new UserProfile();
            return View(w);
        }
        [HttpPost]
        public ActionResult createUser(FormCollection fc)
        {
           bool newsl = Convert.ToBoolean((fc["newsletter"]));
           UserProfile temp = Service.Service.createUser(fc["name"], (fc["pass"]), fc["Email"], fc["adress"], fc["zipcode"], newsl);
           
           return RedirectToAction("userProfile", temp);
        }
        public ActionResult userProfile(int id)
        {
            UserProfile temp = Service.Service.findUser(id);
            return View(temp);
        }
    }
}