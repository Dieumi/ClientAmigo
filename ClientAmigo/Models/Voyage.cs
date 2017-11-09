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

namespace ClientAmigo.Models
{
    public class Voyage
    {
       
        public string PostData { get; set; }
        private string page = "http://localhost:8090/voyage";

      
        public string createVoyage(string id,string arrive, string depart, string heure, string date,int nbplace,double prix,string typevoiture)
        {
            var request = (HttpWebRequest)WebRequest.Create(page);

            request.Method = HttpVerbs.Post.ToString();
            request.ContentLength = 0;
            request.ContentType = "application/json";
            PostData = "{ \"idUser\":\"" + id + "\",\"nbplace\":\"" + nbplace + "\",\"depart\":\"" + depart + "\",\"arrive\":\"" + arrive + "\",\"typeVoiture\":\""+typevoiture+ "\",\"price\":\"" + prix + "\",\"heureDep\":\"" + heure + "\",\"date\":\"" + date + "\"}";

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