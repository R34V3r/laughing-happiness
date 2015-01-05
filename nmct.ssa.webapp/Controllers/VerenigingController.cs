using nmct.ba.cashlessproject.model.Web;
using nmct.ssa.webapp.DataAcces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ssa.webapp.Controllers
{
    [Authorize]
    public class VerenigingController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            List<Vereniging> verenigingen = new List<Vereniging>();
            verenigingen = VerenigingDA.GetVerenigingen();
            return View(verenigingen);
        }

        [HttpGet]
        public ActionResult Nieuw()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Nieuw(Vereniging vereniging)
        {
            int rowsaffected = VerenigingDA.NieuweVereniging(vereniging);
            if (rowsaffected == 0)
                return View("Error");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Bewerken(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Vereniging vereniging = new Vereniging();
            vereniging = VerenigingDA.GetVerenigingById(id.Value);

            return View(vereniging);
        }

        [HttpPost]
        public ActionResult Bewerken(Vereniging vereniging)
        {
            int rowsaffected = VerenigingDA.UpdateVereniging(vereniging);
            if (rowsaffected == 0)
                return View("Error");

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (!id.HasValue)
                return RedirectToAction("Index");

            Vereniging vereniging = new Vereniging();
            vereniging = VerenigingDA.GetVerenigingById(id.Value);

            return View(vereniging);
        }

    }
}