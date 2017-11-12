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
    public class Voyage
    {
        public string id { get; set; }

        public string idUser { get; set; }

        public int nbplace { get; set; }

        public string depart { get; set; }

        public string arrive { get; set; }

        public string typeVoiture { get; set; }
        public double price { get; set; }
        public string note { get; set; }
        public string date { get; set; }
        public string heureDep { get; set; }
        private string verb;
        private string PostData { get; set; }
        private string page = "https://amigoapi.herokuapp.com/voyage";
       // private string page = "http://localhost:8090/voyage";

        public string MakeReq(string data,string pagesuite,HttpStatusCode status)
        {
            var request = (HttpWebRequest)WebRequest.Create(page+pagesuite);

            request.Method = verb;
            request.ContentLength = 0;
            request.ContentType = "application/json";
            var encoding = new UTF8Encoding();
            var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(data);
            request.ContentLength = bytes.Length;

            using (var writeStream = request.GetRequestStream())
            {
                writeStream.Write(bytes, 0, bytes.Length);
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
        public string createVoyage(string id,string arrive, string depart, string heure, string date,int nbplace,double prix,string typevoiture,string note)
        {
            verb = HttpVerbs.Post.ToString();
            PostData = "{ \"idUser\":\"" + id + "\",\"nbplace\":\"" + nbplace + "\",\"depart\":\"" + depart + "\",\"arrive\":\"" + arrive + "\",\"typeVoiture\":\""+typevoiture+ "\",\"price\":\"" + prix + "\",\"heureDep\":\"" + heure + "\",\"date\":\"" + date + "\",\"note\":\"" + note + "\"}";
            string response= MakeReq(PostData,null,HttpStatusCode.Created);
            return response;
        }
        public string getListVoyage( string arrive, string depart,string heureDep,string date)
        {
            verb = HttpVerbs.Post.ToString();
            PostData = "{\"depart\":\"" + depart + "\",\"arrive\":\"" + arrive + "\",\"heureDep\":\"" + heureDep + "\",\"date\":\"" + date + "\"}";
            string response = MakeReq(PostData,"/getList",HttpStatusCode.OK);
            return response;
        }
        public string getListVoyageById(List<string> idlist)
        {
            verb = HttpVerbs.Post.ToString();
            PostData = JsonConvert.SerializeObject(idlist);
            string response = MakeReq(PostData, "/getListByUser", HttpStatusCode.OK);
            return response;
        }
        public string getListVoyageByIdUser(string idUser)
        {
            verb = HttpVerbs.Post.ToString();
            PostData = idUser;
            string response = MakeReq(PostData, "/getListById", HttpStatusCode.OK);
            return response;
        }
        public string updateVoyage(string idvoyage)
        {
            verb = HttpVerbs.Put.ToString();
            PostData =  idvoyage;
            string responsevalue = MakeReq(PostData, "/reduce", HttpStatusCode.OK);
            return responsevalue;
        }
    }
}