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
    public class LogboekController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Kassa> kassas = new List<Kassa>();
            kassas = KassaDA.GetKassas();
            ViewBag.Kassas = kassas;

            List<LogboekPM> logboek = new List<LogboekPM>();
            logboek = LogboekDA.GetLogboek();

            return View(logboek);
        }

        [HttpGet]
        public ActionResult Zoek(int kassa)
        {
            ViewBag.Titel = "Logboek van " + kassa;

            List<LogboekPM> logboek = new List<LogboekPM>();
            logboek = LogboekDA.GetLogboekById(kassa);

            return View(logboek);
        }

    }
}