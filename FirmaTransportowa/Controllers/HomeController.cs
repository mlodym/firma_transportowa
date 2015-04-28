using FirmaTransportowa.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FirmaTransportowa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Rozklad()
        {
            using (FirmaTransportowa.Models.firmatransportowaEntities dc = new firmatransportowaEntities())
            {
                List<SelectListItem> przystanki = new List<SelectListItem>();
                przystanki.Add(new SelectListItem { Text = "Wybierz przystanek", Value = "0", Selected = true });
                foreach (PRZYSTANEK p in dc.PRZYSTANKI)
                {
                        przystanki.Add(new SelectListItem { Text = p.PRK_NAZWA, Value = p.PRK_ID.ToString()});
                }

                ViewBag.Przystanki = przystanki;
                //ViewData["przystanki"] = przystanki;
                //ViewData["godziny"] = godziny;
                return View();
            }
        }

        [HttpGet]
        public ActionResult WybranyRozklad(string _id)
        {

            int _wyb = Convert.ToInt32(_id);
            string _przystanek ="";
            List<string> godzinyKRK = new  List<string>();
            List<string> godzinyKAT = new List<string>();
            List<SelectListItem> przystanki = new List<SelectListItem>();


            using (FirmaTransportowa.Models.firmatransportowaEntities dc = new firmatransportowaEntities())
            {
                foreach (var g in dc.PRZYSTANKI_NA_TRASIE.Where(h => h.PRK_ID == _wyb && h.PRE_KIERUNEK == "KRK"))
                {
                    godzinyKRK.Add((g.PRE_ODJAZD.ToString()).Substring(0,5));
                }

                foreach (var g in dc.PRZYSTANKI_NA_TRASIE.Where(h => h.PRK_ID == _wyb && h.PRE_KIERUNEK == "KAT"))
                {
                    godzinyKAT.Add((g.PRE_ODJAZD.ToString()).Substring(0,5));
                }

                foreach (PRZYSTANEK p in dc.PRZYSTANKI)
                {
                    if (p.PRK_ID == _wyb)
                    {
                        _przystanek = p.PRK_NAZWA;
                        przystanki.Add(new SelectListItem { Text = p.PRK_NAZWA, Value = p.PRK_ID.ToString(), Selected = true });
                    }
                    else
                    {
                        przystanki.Add(new SelectListItem { Text = p.PRK_NAZWA, Value = p.PRK_ID.ToString(), Selected = false });
                    }
                }
            }
            ViewBag.Przystanki = przystanki;
            ViewBag.GodzinyKRK = godzinyKRK;
            ViewBag.GodzinyKAT = godzinyKAT;
            ViewBag.Wybrany = _przystanek;

            return View();

        }
    }
}
