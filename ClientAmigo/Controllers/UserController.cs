using ClientAmigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientAmigo.Controllers
{
    public class UserController : Controller
    {
        User user = new User();
        // GET: User
        public ActionResult Index()
        {
           string result = user.findUser(Session["login"].ToString());
            var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
 
            User userresponse = ser.Deserialize<User>(result);
            ViewBag.user = userresponse;
                return View();
        }
    }
}