using ClientAmigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientAmigo.Controllers
{
    public class VoyageController : Controller
    {
        private Voyage voyage = new Voyage();
        private Ville ville = new Ville();
        private List<Ville> listv = new List<Ville>();
        // GET: Voyage
        public ActionResult Index()
        {
            string listville = ville.getAllville();
            var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            listv= ser.Deserialize<List<Ville>>(listville);
            ViewBag.listv = listv;
            
            return View();
        }
        public ActionResult search()
        {
            string arr = Request.Form["arr"];
            string dep = Request.Form["dep"];
            string date = Request.Form["date"];
            string heure = Request.Form["heure"];
            var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Voyage> ListVoyage=ser.Deserialize<List<Voyage>>(voyage.getListVoyage(arr, dep,heure,date));
            ViewBag.listvoyage = ListVoyage;
            ViewBag.length = ListVoyage.Count;
            return View();
        }
        public ActionResult create()
        {
            string listville = ville.getAllville();
            var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            listv = ser.Deserialize<List<Ville>>(listville);
            ViewBag.listv = listv;
            Console.WriteLine(Session["id"]);
            int nbplace = Convert.ToInt32(Request.Form["nbplace"]);
            string dep = Request.Form["depart"];
            string arr = Request.Form["arrive"];
            string type = Request.Form["type"];
            double prix = Convert.ToDouble(Request.Form["prix"]);
            string date = Request.Form["date"];
            string heure = Request.Form["heure"];
            string id = Convert.ToString(Session["id"]);

            var result = voyage.createVoyage(id, arr, dep, heure, date, nbplace, prix, type);

            if (result !="")
            {
                ViewData["msg"] = "Voyage Crée";
            }
            return View("index");
        }
    }
}