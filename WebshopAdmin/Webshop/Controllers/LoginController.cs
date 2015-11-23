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
        //[HttpPost]
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Index(FormCollection fc)
        {
            String name = fc["name"];
            String pass = fc["password"];
            try {
                UserProfile temp = Service.Service.validateLogin(pass, name);
                Service.Service.createShoppingCart(temp);
                return RedirectToAction("Index","Webshop", new { id = temp.id });
            }
            catch
            {
                    ModelState.AddModelError("LoginError", "Log in er ikke korrekt prøv igen");
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
           bool newsl = fc["newsletter"].Contains("true");
           UserProfile temp = Service.Service.createUser(fc["name"], (fc["pass"]), fc["Email"], fc["adress"], fc["zipcode"], newsl);
            
            return RedirectToAction("Index", "Webshop", new { id = temp.id });
        }
        
    }
}