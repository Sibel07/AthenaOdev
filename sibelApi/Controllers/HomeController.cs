using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sibelApi.Entity;

namespace sibelApi.wwwroot
{
    public class HomeController : Controller
    {
        Login login;
        public IActionResult Index()
        {
            login = new Login();

            login = JsonConvert.DeserializeObject<Login>(sibel.Login("test", "123456", "146116"));
            List<RestoranList> list = new List<RestoranList>();
            list = sibel.RestoranListesiGetir(login.token);
            return View(list);
        }

        public JsonResult AlacaCarteList(string unitID, string unitKey, string DateTime)
        {
            login = JsonConvert.DeserializeObject<Login>(sibel.Login("test", "123456", "146116"));
            var alacartList = sibel.AlacarteListeGetir(unitID, unitKey, Convert.ToDateTime(DateTime), login.token);
            return Json(new { result = alacartList, date = Convert.ToDateTime(DateTime).ToString("dd-MM-yyyy") });
        }

        public JsonResult RezervasyonSave(Rezervasyon model)
        {
            login = JsonConvert.DeserializeObject<Login>(sibel.Login("test", "123456", "146116"));

            var result = sibel.RezervasyonYap(model, login.token);

            return Json(new { result = result });
        }
    }
}