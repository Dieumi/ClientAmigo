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
    public class TypeV
    {
        public string id { get; set; }
        public string value { get; set; }
        private string PostData { get; set; }
        private string verb;
        private string page = "https://amigoapi.herokuapp.com/type";
       // private string page = "http://localhost:8090/type";
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
        public string getListType()
        {
            verb = HttpVerbs.Get.ToString();
            string response = MakeReq(PostData, "", HttpStatusCode.OK);
            return response;
        }
        public string create(string idtype,string idvoyage)
        {
            verb = HttpVerbs.Post.ToString();
            PostData = "{\"idType\":\"" + idtype + "\",\"idvoyage\":\"" + idvoyage + "\"}";
            string response = MakeReq(PostData, "", HttpStatusCode.Created);
            return response;
        }
    }
}