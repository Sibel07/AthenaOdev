using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Text;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System.Dynamic;

namespace sibelApi
{
    public static class sibel
    {
        public static string apiLink = "http://213.142.146.52:6161/athenaservices/alacarte/api/v2/";
        private static readonly HttpClient client = new HttpClient();
        public static string postJSON(string link, string values)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(link);

                var postData = values;
                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/json";
                request.ContentLength = data.Length;

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var responseString = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    //return responseString; //status kodu değişik olsa bile response almak istiyorsak eğer.
                    return "-1";
                }
                else
                {
                    return "-1";
                }
            }

        }

        public static string postHeader(string link, WebHeaderCollection headerData) //GET işlemi ile aynı sadece değerler header'den gidiyor.
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(link);

                request.Method = "GET";
                request.ContentType = "application/json";
                request.Headers.Add(headerData);

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var responseString = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    //return responseString; //status kodu değişik olsa bile response almak istiyorsak eğer.
                    return "-1";
                }
                else
                {
                    return "-1";
                }
            }
        }

        public static string Login(string username, string password, string appKey)
        {
            string loginLink = apiLink + "user/login";

            dynamic degerler = new ExpandoObject();
            degerler.username = username;
            degerler.password = password;
            degerler.appKey = appKey;
            string jsonString = JsonConvert.SerializeObject(degerler);

            string result = postJSON(loginLink, jsonString);

            return result;
        }

        public static List<Entity.RestoranList> RestoranListesiGetir(string token)
        {
            var ret = new List<Entity.RestoranList>();

            string loginLink = apiLink + "admin/unit/list?code=GGR"; //GGR yazan kısım burda hotel kodu


            return "x";
        }


    }
}
