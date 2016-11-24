using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TesteAgence.Models;

namespace TesteAgence.Controllers
{
    public class AgenceController : Controller
    {
        // GET: Agence
        private static Agence _agence = new Agence();

        public ActionResult Index()
        {
            return View(_agence.listaAgence);
        }

        public ActionResult AdicionaMarcacao()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdicionaMarcacao(AgenceModel _agenceModel)
        {
            _agence.CriaMarcacao(_agenceModel);
            return View();
        }

    }
}