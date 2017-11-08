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
        private Ville ville = new Ville();
       
        // GET: Voyage
        public ActionResult Index()
        {
            string listville = ville.getAllville();
            return View();
        }
    }
}