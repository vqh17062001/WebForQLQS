using Microsoft.AspNetCore.Mvc;
using WebForQLQS.Models;

namespace WebForQLQS.Controllers
{
    public class TieuDoanController : Controller
    {
        public IActionResult viewTieuDoan()


        {

            datalinkmodel link = new datalinkmodel("viewQSTieuDoan");

            ViewBag.linkmodel = link;
            return View();
           
        }



        [HttpGet]



        public IActionResult linkviewQSDonVid()
        {

            datalinkmodel link = new datalinkmodel("viewQSTieuDoan");

            ViewBag.linkmodel = link;
            return View("ViewTieuDoan");

        }


        public IActionResult linkviewAddInfd()
        {

            datalinkmodel link = new datalinkmodel("viewAddInfd");

            ViewBag.linkmodel = link;
            return View("ViewTieuDoan");

        }



        public IActionResult linkviewForAnalystd()
        {

            datalinkmodel link = new datalinkmodel("viewForAnalystd");

            ViewBag.linkmodel = link;
            return View("ViewTieuDoan");

        }



        public IActionResult linkviewBaoCaod()
        {

            datalinkmodel link = new datalinkmodel("viewBaoCaod");

            ViewBag.linkmodel = link;
            return View("ViewTieuDoan");

        }

    }
}
