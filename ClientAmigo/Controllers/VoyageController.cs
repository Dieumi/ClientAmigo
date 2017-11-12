using ClientAmigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ClientAmigo.Controllers
{
    public class VoyageController : Controller
    {
        private Voyage voyage = new Voyage();
        private Ville ville = new Ville();
        private TypeV typee = new TypeV();
        private typeVoyage tv = new typeVoyage();
        private List<Ville> listv = new List<Ville>();
        private List<TypeV> listT = new List<TypeV>();
        private List<typeVoyage> listTvoyage = new List<typeVoyage>();
        // GET: Voyage
        public ActionResult Index()
        {
            if (Session["id"]!=null)
            {
                string listville = ville.getAllville();
                string listtype = typee.getListType();
                var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
                listv = ser.Deserialize<List<Ville>>(listville);
                listT = ser.Deserialize<List<TypeV>>(listtype);
                ViewBag.listv = listv;
                ViewBag.list = listT;

                return View();
            }
            else
            {
                return View("Connexion");
            }
           
        }
        public ActionResult search()
        {
            string arr = Request.Form["arr"];
            string dep = Request.Form["dep"];
            string date = Request.Form["date"];
            string heure = Request.Form["heure"];
            var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            string listtype = typee.getListType();
            List<Voyage> ListVoyage=ser.Deserialize<List<Voyage>>(voyage.getListVoyage(arr, dep,heure,date));
            listTvoyage = ser.Deserialize<List<typeVoyage>>(tv.getListTypeVoyage());
            listT = ser.Deserialize<List<TypeV>>(listtype);
            ViewBag.listvoyage = ListVoyage;
            ViewBag.length = ListVoyage.Count;
            ViewBag.condition = listTvoyage;
            ViewBag.list = listT;
            return View();
        }
        public ActionResult create()
        {
            string listville = ville.getAllville();
            var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            string listtype = typee.getListType();
            listv = ser.Deserialize<List<Ville>>(listville);
            listT = ser.Deserialize<List<TypeV>>(listtype);
            ViewBag.listv = listv;
            ViewBag.list = listT;
            Console.WriteLine(Session["id"]);
            int nbplace = Convert.ToInt32(Request.Form["nbplace"]);
            Encoding utf = Encoding.UTF8;
            Byte[] depb = utf.GetBytes(Request.Form["depart"]);
            string dep = utf.GetString(depb);
            Byte[] arrb = utf.GetBytes(Request.Form["arrive"]);
            string arr = utf.GetString(arrb);
            string type = Request.Form["type"];
            string typeV = Request.Form["typeV"];
            double prix = Convert.ToDouble(Request.Form["prix"]);
            string date = Request.Form["date"];
            string heure = Request.Form["heure"];
            string id = Convert.ToString(Session["id"]);
            string note = Request.Form["note"];
            var result = voyage.createVoyage(id, arr, dep, heure, date, nbplace, prix, type,note);
            Voyage v = ser.Deserialize<Voyage>(result);
           string[] typeVA = typeV.Split(',');
            foreach(string t in typeVA)
            {
                var result2 = tv.create(t,v.id);
            }
            
            if (result !="")
            {
                ViewData["msg"] = "Voyage Crée";
            }
            return View("index");
        }
        public ContentResult delete()
        {
            string response = voyage.deleteVoyage(Request.Form["id"]);

            return Content(response);
        }

    }
}