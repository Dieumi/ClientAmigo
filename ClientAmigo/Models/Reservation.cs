using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ClientAmigo.Models
{
    public class Reservation
    {
        public string id { get; set; }
        public string idVoyage { get; set; }
        public string idUser { get; set; }
        public double note { get; set; }
        private string verb;
        private string PostData { get; set; }
        private string page = "https://amigoapi.herokuapp.com/uservoyage";
        //private string page = "http://localhost:8090/uservoyage";

        public string MakeReq(string data, string pagesuite,HttpStatusCode status)
        {
            var request = (HttpWebRequest)WebRequest.Create(page + pagesuite);

            request.Method = verb;
            request.ContentLength = 0;
            request.ContentType = "application/json";
            if (verb != "Get")
            {
                var encoding = new UTF8Encoding();
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

        public string makeABook(string idvoyage,string iduser)
        {
            verb = HttpVerbs.Post.ToString();
            PostData = "{\"idVoyage\":\"" + idvoyage + "\",\"idUser\":\"" + iduser + "\"}";
            string responsevalue = MakeReq(PostData, "",HttpStatusCode.Created);
            return responsevalue;
        }
        public string findAllById(string idUser)
        {
            verb = HttpVerbs.Get.ToString();
          
            string responsevalue = MakeReq(PostData, "/"+idUser, HttpStatusCode.OK);
            return responsevalue;
        }
        public string updatenote(string idresa,string idvoyage,string iduser,double note)
        {
            verb = HttpVerbs.Put.ToString();
            PostData = "{\"id\":\"" + idresa + "\",\"idVoyage\":\"" + idvoyage + "\",\"idUser\":\"" + iduser + "\",\"note\":\"" + note.ToString().Replace(",",".") + "\"}";
            string responsevalue = MakeReq(PostData, "", HttpStatusCode.OK);
            return responsevalue;
        }
    }
}