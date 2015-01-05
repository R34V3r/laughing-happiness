using nmct.ba.cashlessproject.model.Web;
using nmct.ssa.webapp.DataAcces;
using nmct.ssa.webapp.PresentationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.webapp.Controllers
{
    [Authorize]
    public class KassaController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<KassaPM> alleKassas = new List<KassaPM>();
            alleKassas = KassaDA.GetAlleKassas();

            List<KassaPM> ToegekendeKassas = new List<KassaPM>();
            List<KassaPM> BeschikbareKassas = new List<KassaPM>();

            foreach (KassaPM item in alleKassas)
            {
                if (item.vereniging.OrganisationName != "")
                    ToegekendeKassas.Add(item);
                else
                    BeschikbareKassas.Add(item);
            }

            ViewBag.Kassas = alleKassas;
            ViewBag.BeschikbareKassas = BeschikbareKassas;
            ViewBag.ToegekendeKassas = ToegekendeKassas;

            List<Vereniging> verenigingen = new List<Vereniging>();
            verenigingen = VerenigingDA.GetVerenigingen();
            ViewBag.Verenigingen = verenigingen;

            return View();
        }

        [HttpGet]
        public ActionResult Zoek(int vereniging)
        {
            List<KassaPM> kassas = new List<KassaPM>();
            kassas = KassaDA.GetKassasByVereniging(vereniging);
            if (kassas.Count != 0)
                ViewBag.Titel = kassas.Count + " kassa(s) gevonden van " + kassas[0].vereniging.OrganisationName;
            else
                ViewBag.Titel = "Geen zoekresultaten gevonden.";

            return View(kassas);
        }

        [HttpGet]
        public ActionResult Nieuw()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nieuw(Kassa kassa)
        {
            int rowsaffected = KassaDA.NieuweKassa(kassa);
            if (rowsaffected == 0)
                return View("Error");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Wijzigen()
        {
            List<Kassa> kassas = new List<Kassa>();
            kassas = KassaDA.GetKassas();
            ViewBag.Kassas = kassas;

            List<Vereniging> verenigingen = new List<Vereniging>();
            verenigingen = VerenigingDA.GetVerenigingen();
            ViewBag.Verenigingen = verenigingen;

            return View();
        }

        [HttpPost]
        public ActionResult Wijzigen(int kassa, int vereniging)
        {
            List<KassaPM> toekenning = new List<KassaPM>();
            toekenning = KassaDA.CheckKassaToekenning(kassa, vereniging);

            if (toekenning.Count == 0) //kassa nog niet toegekend aan vereniging
            {
                int rowsaffected = KassaDA.KassaToevoegenVereniging(kassa, vereniging);
            }
            else if (toekenning.Count == 1)//kassa al toegekend aan vereniging => kassa wijzigen van vereniging
            {
                int rowsaffected = KassaDA.UpdateVerenigingKassa(vereniging, kassa);
            }

            return RedirectToAction("Index", "Kassa");
        }

    }
}