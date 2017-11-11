using ClientAmigo.Models;
using Newtonsoft.Json;
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
        Voyage v = new Voyage();
        Reservation res = new Reservation();
        // GET: User
        public ActionResult Index()
        {
           string result = user.findUser(Session["login"].ToString());
            string resa = res.findAllById(Session["id"].ToString());
            var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            
            User userresponse = ser.Deserialize<User>(result);
            List<Reservation> listresa = ser.Deserialize<List<Reservation>>(resa);
            if (listresa != null)
            {
                List<string> idlist = listresa.Select(z => z.idVoyage).ToList();
                string listv = v.getListVoyageById(idlist);
                List<Voyage> listvoyage = ser.Deserialize<List<Voyage>>(listv);
                ViewBag.list = listvoyage;
                ViewBag.listres = listresa;
            }
            
            ViewBag.user = userresponse;
          
            return View();
        }
        public ContentResult updatenote()
        {
            var result = user.findUser(Session["login"].ToString());
            var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            User userresponse = ser.Deserialize<User>(result);
            userresponse.nbvoyage = userresponse.nbvoyage + 1;
            Console.WriteLine(Request.Form);
            Double notechoosen = Convert.ToDouble(Request.Form["note"]);
            string idresa = Request.Form["idresa"];
            userresponse.note = Convert.ToDouble(userresponse.note + notechoosen)/userresponse.nbvoyage;
            var update = user.updateUser(userresponse);
            User userupdate = ser.Deserialize<User>(update);
            var response = res.updatenote(idresa,Request.Form["idvoyage"], Session["id"].ToString(), notechoosen);
            Reservation resresponse = ser.Deserialize<Reservation>(response);
            if (userupdate != null)
            {
                return Content("updated");
            }
            else
            {
                return Content("notUpdated");
            }
         
        }
    }
}