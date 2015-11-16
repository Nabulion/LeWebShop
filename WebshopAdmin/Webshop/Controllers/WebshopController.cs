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
        
    }
}