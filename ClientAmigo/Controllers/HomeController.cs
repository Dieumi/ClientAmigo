using ClientAmigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
namespace ClientAmigo.Controllers
{
    public class HomeController : Controller
    {
        private Auth auth = new Auth();
        private User user=new User();
        private SecurityController security=new SecurityController();
        private string key = "42";
        public ActionResult Index()
        {
            
            
            return View();
        }
        public ActionResult Inscription()
        {
            
            return View();
        }
        public ActionResult createUser()
        {
            string email;
            string login;
            string password;
            string cpassword;
            string name;
            string lastname;
            email = Request.Form["email"];
            login = Request.Form["login"];
            password = security.Encrypt(Request.Form["password"], key);
            cpassword = security.Encrypt(Request.Form["password"], key);
            name = Request.Form["name"];
            lastname = Request.Form["lastname"];
            if (cpassword != password)
            {
                ViewData["message"] = "mot de passe différents";
                return View("Inscription");
            }
            else
            {
                string exist = auth.postAuthExistByLogin(login);
                if (exist == "") {
                    string result = user.createUser(name, lastname, email, login);
                    string resultAuth = auth.postAuthAdd(login, password, "/add");
                }
                else
                {
                    ViewData["exist"] = "Cet utilisateur existe déjà.";
                }
              
            }

            return View("index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}