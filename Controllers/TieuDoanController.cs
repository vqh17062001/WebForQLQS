using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using WebForQLQS.Data;
using WebForQLQS.Models;

//using System.Collections.Generic;

namespace WebForQLQS.Controllers
{
    public class TieuDoanController : Controller
    {

        private HtqlqsContext _context = new HtqlqsContext();

        static object ten;
        static string searchvalue;
        public IActionResult viewTieuDoan(int page = 1)


        {

            int pageSize = 10;



            var quannhanlist = _context.QuanNhans.ToList();
            var pagedItems = quannhanlist.Skip((page - 1) * pageSize).Take(pageSize);

            var model = new PagedViewModel<QuanNhan>
            {
                Items = pagedItems.ToList(),
                TotalItems = quannhanlist.Count,
                CurrentPage = page,
                PageSize = pageSize
            };

            datalinkmodel link = new datalinkmodel("viewQSTieuDoan");

            

            var item = TempData["idsearch"] as string;
           
            if (item != null)
            {
                searchvalue = item;

            }
            List<QuanNhan> pageitemsearch = new List<QuanNhan>();

            if (searchvalue != null) { 
                foreach (var qn in quannhanlist) {
                    if (searchvalue == qn.DonVi||searchvalue==qn.HoTen) { 
                        pageitemsearch.Add(qn);
                    
                    }

                }
                pagedItems = pageitemsearch.Skip((page - 1) * pageSize).Take(pageSize);

                model.Items = pagedItems.ToList();
                model.TotalItems = pageitemsearch.Count;
                model.CurrentPage = page;
                model.PageSize = pageSize;
            }

            ViewBag.linkmodel = link;
            ten = TempData["name"];

            ViewData["name"] = ten;

            return View(model);

        }



        [HttpGet]



        public IActionResult linkviewQSDonVid()
        {

            searchvalue = null;
            TempData["name"] = ten;
            return RedirectToAction("viewTieuDoan", "TieuDoan");

        }


        public IActionResult linkviewAddInfd()
        {

            datalinkmodel link = new datalinkmodel("viewAddInfd");

            ViewBag.linkmodel = link;
            ViewData["name"] = ten;
            return View("ViewTieuDoan");

        }



        public IActionResult linkviewForAnalystd()
        {

            datalinkmodel link = new datalinkmodel("viewForAnalystd");

            ViewBag.linkmodel = link;
            ViewData["name"] = ten;
            return View("ViewTieuDoan");

        }



        public IActionResult linkviewBaoCaod()
        {

            datalinkmodel link = new datalinkmodel("viewBaoCaod");

            ViewBag.linkmodel = link;
            ViewData["name"] = ten;
            return View("ViewTieuDoan");

        }


        public IActionResult changeinfd()
        {


            return View();

        }

       

        
        [HttpPost]
        public IActionResult table_searchButtonClick(string table_search)
        {

           

            
            TempData["idsearch"] = table_search;
            return RedirectToAction("viewTieuDoan","TieuDoan");
        }


    }
}
