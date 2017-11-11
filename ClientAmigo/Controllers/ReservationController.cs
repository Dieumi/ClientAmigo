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
        User u = new User();
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
                double price = Convert.ToDouble(Request.Form["price"]);
                string response3 = u.updateUser(iduser, price);
                if(response3!="Pas assez de bif")
                {
                    string response = re.makeABook(idvoyage, iduser);
                    string response2 = v.updateVoyage(idvoyage);
                    ViewBag.msg = "Voyage réservé";
                }
                else
                {
                    ViewBag.msg = "Pas assez de bif";
                }
            
               
               
            }
            else
            {
                ViewBag.msg = "Veuillez vous connectez pour réservé";
            }
        
            return View();
        }
    }
}