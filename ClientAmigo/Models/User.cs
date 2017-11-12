using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace ClientAmigo.Models
{
    public class User
    {
        public string id { get; set; }

        public string name { get; set; }

        public string lastName { get; set; }

        public string email { get; set; }

        public string login { get; set; }

        public string type { get; set; }

        public double credit { get; set; }
        public double note { get; set; }
        public int nbvoyage { get; set; }
        private string PostData { get; set; }
        private string verb;
        private string page = "https://amigoapi.herokuapp.com/user";
        //private string page = "http://localhost:8090/user";
        public string MakeReq(string data, string pagesuite, HttpStatusCode status)
        {
            var request = (HttpWebRequest)WebRequest.Create(page + pagesuite);

            request.Method = verb;
            request.ContentLength = 0;
            request.ContentType = "application/json";
            var encoding = new UTF8Encoding();
          
            if (verb != "Get")
            {
                var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(data);
                request.ContentLength = bytes.Length;
                using (var writeStream = request.GetRequestStream())
                {
                    writeStream.Write(bytes, 0, bytes.Length);
                }
            }



            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != status)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;


            }
        }

        public string updateUser(string id,double price)
        {
            verb = HttpVerbs.Put.ToString();
            PostData = "{ \"idUser\":\"" + id + "\",\"price\":\"" + price + "\"}";
            string response = MakeReq(PostData, "/pay", HttpStatusCode.OK);
            return response;

        }
        public string updateUser(User user)
        {
            verb = HttpVerbs.Put.ToString();
            PostData = JsonConvert.SerializeObject(user);
            string response = MakeReq(PostData, "", HttpStatusCode.OK);
            return response;

        }
        public string findUser(string login)
        {
            verb = HttpVerbs.Get.ToString();
            PostData = login;
            string response = MakeReq(PostData, "/"+login, HttpStatusCode.OK);
            return response;

        }
        public string createUser(string name, string lastname, string email, string login)
        {
            var request = (HttpWebRequest)WebRequest.Create(page);

            request.Method = HttpVerbs.Post.ToString();
            request.ContentLength = 0;
            request.ContentType = "application/json";
            PostData = "{ \"name\":\"" + name + "\",\"lastName\":\"" + lastname + "\",\"login\":\"" + login + "\",\"email\":\"" + email + "\",\"type\":\"user\",\"credit\":\"100\"}";

            var encoding = new UTF8Encoding();
            var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
            request.ContentLength = bytes.Length;

            using (var writeStream = request.GetRequestStream())
            {
                writeStream.Write(bytes, 0, bytes.Length);
            }


            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.Created)
                {
                    var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);
                    throw new ApplicationException(message);
                }

                // grab the response
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream))
                        {
                            responseValue = reader.ReadToEnd();
                        }
                }

                return responseValue;


            }
        }
    }
}