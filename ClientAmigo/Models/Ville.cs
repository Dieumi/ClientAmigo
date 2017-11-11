﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace ClientAmigo.Models
{
    public class Ville
    {
        public string id { get; set; }
        public string name { get; set; }
        public string region { get; set; }
        private string PostData { get; set; }
        private string page = "https://amigoapi.herokuapp.com/ville";
        public string getAllville()
        {
            var request = (HttpWebRequest)WebRequest.Create(page);

            request.Method = HttpVerbs.Get.ToString();
            request.ContentLength = 0;
            request.ContentType = "application/json";
           
            var encoding = new UTF8Encoding();
         

            


            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var responseValue = string.Empty;

                if (response.StatusCode != HttpStatusCode.OK)
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