using ClientAmigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientAmigo.Controllers
{
    public class ReservationController : Controller
    {

        Voyage v = new Voyage();
        Reservation re = new Reservation();
        // GET: Reservation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult book()
        {
            if (Session["id"]!=null)
            {
                string idvoyage = Request.Form["id"];
                string iduser = Session["id"].ToString();
                string response = re.makeABook(idvoyage, iduser);
                string response2 = v.updateVoyage(idvoyage);
            }
            else
            {
                ViewBag.msg = "Veuillez vous connectez pour réservé";
            }
        
            return View();
        }
    }
}