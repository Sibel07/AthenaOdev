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
        public static string postJSON(string link, string values, bool herturluGetir = false)
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
                    if (herturluGetir == true)
                    {
                        var responseString = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                        return responseString;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                else
                {
                    return "-1";
                }
            }

        }

        public static string postHeader(string link, WebHeaderCollection headerData, bool herturlugetir = false) //GET işlemi ile aynı sadece değerler header'den gidiyor.
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
                    if (herturlugetir == true)
                    {
                        var responseString = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                        return responseString;
                    }
                    else
                    {
                        return "-1";
                    }
                }
                else
                {
                    return "-1";
                }
            }
        }

        public static string postHeaderData(string link, string values, WebHeaderCollection headerData, bool herturlugetir = false) //GET işlemi ile aynı sadece değerler header'den gidiyor.
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

                request.Headers.Add(headerData);

                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    if (herturlugetir == true)
                    {
                        var responseString = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                        return responseString;
                    }
                    else
                    {
                        return "-1";
                    }
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
            string restoranLink = apiLink + "admin/unit/list?code=GGR"; //GGR yazan kısım burda hotel kodu
            var headers = new WebHeaderCollection();
            headers.Add("token", token);

            string response = postHeader(restoranLink, headers);

            if (response != "-1")
            {
                var result = JsonConvert.DeserializeObject<List<Entity.RestoranList>>(response);
                return result;
            }
            else
            {
                return null;
            }

        }

        public static List<Entity.RestoranAlacarte> AlacarteListeGetir(string unitID, string unitKey, DateTime tarih, string token)
        {
            string restoranLink = apiLink + String.Format("admin/unit/{0}?key={1}&date={2}", unitID, unitKey, tarih.ToString("yyyy-MM-dd"));
            var headers = new WebHeaderCollection();
            headers.Add("token", token);

            string response = postHeader(restoranLink, headers);

            if (response != "-1")
            {
                if (response.StartsWith("["))
                {
                    var result = JsonConvert.DeserializeObject<List<Entity.RestoranAlacarte>>(response);
                    return result;
                }
                else
                {
                    var result = JsonConvert.DeserializeObject<Entity.RestoranAlacarte>(response);
                    var tmp = new List<Entity.RestoranAlacarte>();
                    tmp.Add(result);
                    return tmp;
                }
            }
            else
            {
                return null;
            }
        }

        public static Entity.RezervasyonResult RezervasyonYap(Entity.Rezervasyon rezervasyon, string token)
        {

            string restoranLink = apiLink + "reservation";
            var headers = new WebHeaderCollection();
            headers.Add("token", token);

            var rezervasyonData = new Entity.Rezervasyon
            {
                hotelId = 1,
                UnitId = 14,
                date = "2018-05-14",
                time = "08:00-23:00",
                reservationAdult = 1,
                reservationChild = 0,
                reservationNote = "Mobilden girilmeyen rezervasyon",
                guest = new Entity.RezervasyonGuest()
                {
                    hotelCode = "GGR",
                    roomNumber = "999",
                    folio = 123,
                    firstName = "Sibel",
                    lastName = "Özkaynak",
                    title = "MR",
                    gender = "0",
                    country = "TR",
                    adult = 1,
                    child = 0,
                    arrivalDate = "2018-04-11",
                    departureDate = "2018-04-20",
                    panTip = "VIP",
                    nights = 10
                }
            };

            string rezervasyonDataJSON = JsonConvert.SerializeObject(rezervasyonData);

            string response = postHeaderData(restoranLink, rezervasyonDataJSON, headers, true);

            return JsonConvert.DeserializeObject<Entity.RezervasyonResult>(response);
        }
    }
}
