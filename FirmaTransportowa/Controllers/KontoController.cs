using FirmaTransportowa.Filters;
using FirmaTransportowa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace FirmaTransportowa.Controllers
{
    public class KontoController : Controller
    {
        //
        // GET: /Konto/

        #region Logowanie
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(OSOBA u)
        {
            // this action is for handle post (login)
            if (ModelState.IsValid) // this is check validity
            {
                using ( FirmaTransportowa.Models.firmatransportowaEntities dc = new firmatransportowaEntities())
                {
                    var v = dc.OSOBY.Where(a => a.OSO_LOGIN.Equals(u.OSO_LOGIN) && a.OSO_HASLO.Equals(u.OSO_HASLO)).FirstOrDefault();
                    if (v != null)
                    {
                        Session["LogedUserID"] = v.OSO_ID.ToString();
                        Session["LogedUserFullname"] = v.OSO_IMIE.ToString() + " " + v.OSO_NAZWISKO.ToString();
                        FormsAuthentication.SetAuthCookie(v.OSO_LOGIN.ToString(), false);
                        return RedirectToAction("AfterLogin");
                    }
                    ModelState.AddModelError("", "Podana nazwa lub hasło sa nieprawidłowe.");
                }
            }
            return View(u);
        }

        public ActionResult AfterLogin()
        {
            if (Session["LogedUserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        #endregion Logowanie

        #region Rejestracja
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(OSOBA U)
        {
            if (ModelState.IsValid)
            {
                using (FirmaTransportowa.Models.firmatransportowaEntities dc = new firmatransportowaEntities())
                {
                    //you should check duplicate registration here 
                    //OMG to zakrawa na czyste szleństwo :(
                    if (dc.OSOBY.Max(u => (int?)u.OSO_ID) != null)
                        U.OSO_ID = dc.OSOBY.Max(u => u.OSO_ID) + 1;
                    else
                        U.OSO_ID = 1;
                    DANE_TELEADRESOWE D = new DANE_TELEADRESOWE();
                    if (dc.DANE_TELEADRESOWE.Max(u => (int?)u.DAN_ID) != null)
                        D.DAN_ID = dc.DANE_TELEADRESOWE.Max(u => (int)u.DAN_ID) + 1;
                    else D.DAN_ID = 1;
                    U.DANE_TELEADRESOWE.DAN_ID = D.DAN_ID;
                    D.DAN_EMAIL = U.DANE_TELEADRESOWE.DAN_EMAIL;
                    D.DAN_KOD_POCZTOWY = U.DANE_TELEADRESOWE.DAN_KOD_POCZTOWY;
                    D.DAN_MIASTO = U.DANE_TELEADRESOWE.DAN_MIASTO;
                    D.DAN_TELEFON = U.DANE_TELEADRESOWE.DAN_TELEFON;
                    D.DAN_ULICA_NR_LOKALU = U.DANE_TELEADRESOWE.DAN_ULICA_NR_LOKALU;                               
                    //dc.DANE_TELEADRESOWE.Add(D);
                    dc.OSOBY.Add(U);  
                    dc.SaveChanges();
                    ModelState.Clear();
                    U = null;
                    ViewBag.Message = "Successfully Registration Done";
                }
            }
            return View(U);
        }

        #endregion Rejestracja

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

    }
}
