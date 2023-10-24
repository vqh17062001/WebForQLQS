using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebForQLQS.Models;

namespace WebForQLQS.Controllers
{



   

    public class DaiDoiController : Controller
    {
        // GET: DaiDoiController

       
        public IActionResult ViewDaiDoi()
        {

            datalinkmodel link = new datalinkmodel("viewQSDonVi");

            ViewBag.linkmodel = link;

            return View();
        }

        // GET: DaiDoiController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DaiDoiController/Create
        public ActionResult Create()
        {
            return View();
        }

       

        
        // POST: DaiDoiController/Create
        [HttpGet]

       

        public IActionResult linkviewQSDonVi() {

            datalinkmodel link = new datalinkmodel("viewQSDonVi");

            ViewBag.linkmodel = link;
            return View("ViewDaiDoi");
        
        }


        public IActionResult linkviewAddInf()
        {

            datalinkmodel link = new datalinkmodel("viewAddInf");

            ViewBag.linkmodel = link;
            return View("ViewDaiDoi");

        }



        public IActionResult linkviewForAnalyst()
        {

            datalinkmodel link = new datalinkmodel("viewForAnalyst");

            ViewBag.linkmodel = link;
            return View("ViewDaiDoi");

        }



        public IActionResult linkviewBaoCao()
        {

            datalinkmodel link = new datalinkmodel("viewBaoCao");

            ViewBag.linkmodel = link;
            return View("ViewDaiDoi");

        }
    }
}
