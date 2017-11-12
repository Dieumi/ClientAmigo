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
        private List<Ville> listv = new List<Ville>();
        private List<TypeV> listT = new List<TypeV>();
        private TypeV typee = new TypeV();
        private typeVoyage tv = new typeVoyage();
        private List<typeVoyage> listTvoyage = new List<typeVoyage>();
        // GET: User
        public ActionResult Index()
        {
           string result = user.findUser(Session["login"].ToString());
            string resa = res.findAllById(Session["id"].ToString());
            string listtype = typee.getListType();
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
            listTvoyage = ser.Deserialize<List<typeVoyage>>(tv.getListTypeVoyage());
            listT = ser.Deserialize<List<TypeV>>(listtype);
            List<Voyage> listvoyageUser;
            string response= v.getListVoyageByIdUser(Session["id"].ToString());
            listvoyageUser = ser.Deserialize<List<Voyage>>(response);
            ViewBag.user = userresponse;
            ViewBag.listvoyage = listvoyageUser;
            ViewBag.listT = listT;
            ViewBag.length = listvoyageUser.Count;
            ViewBag.condition = listTvoyage;
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