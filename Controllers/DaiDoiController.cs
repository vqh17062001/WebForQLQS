using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebForQLQS.Data;
using WebForQLQS.Models;

namespace WebForQLQS.Controllers
{



   

    public class DaiDoiController : Controller
    {
        // GET: DaiDoiController
        private HtqlqsContext _context = new HtqlqsContext();
        static  string  idten;
        public IActionResult ViewDaiDoi(int page = 1)
        {
            //////////////
            ///
            datalinkmodel link = new datalinkmodel("viewQSDonVi");

            ViewBag.linkmodel = link;
            if (TempData["name"] != null)
            {

                idten = TempData["name"] as string;
            }
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            ///////////
            ///
            string madonvi= null;
            var qn_dvlist = _context.QuannhanDonvis.ToList();
            foreach (var qndv in qn_dvlist) {
                if (qndv.MaQuanNhan == idten) {
                    madonvi = qndv.MaDonVi;
                    break;
                }
            
            }

            List<string> listmaquannhan = new List<string>();

            foreach (var qndv in qn_dvlist) {

                if (qndv.MaDonVi == madonvi) {
                    listmaquannhan.Add(qndv.MaQuanNhan);
                }
            
            }
            var quannhanlistfull = _context.QuanNhans.ToList();
            List<QuanNhan> quannhandaidoilist = new List<QuanNhan>();
            foreach (var maqn in listmaquannhan) {
               
                foreach (var qn in quannhanlistfull) {
                    if (qn.MaQuanNhan == maqn) {
                        quannhandaidoilist.Add(qn);
                        
                    
                    }
                
                }
            
            }


            ///////
            int pageSize = 10;



            var qn_cvlist = _context.QuannhanChucvus.ToList();

            var pagedItems = quannhandaidoilist.Skip((page - 1) * pageSize).Take(pageSize);

            var model = new PagedViewModel<QuanNhan>
            {
                Items = pagedItems.ToList(),
                TotalItems = quannhandaidoilist.Count,
                CurrentPage = page,
                PageSize = pageSize
            };

            ViewData["ttDonVi"] = qn_dvlist;
            ViewData["ttChucVu"] = qn_cvlist;
            ViewData["mess"] = TempData["mess"];
            ViewData["donvi"] = madonvi;
            ////////
            ///





            return View(model);
        }

       

        
        // POST: DaiDoiController/Create
        [HttpGet]

       

        public IActionResult linkviewQSDonVi() {

            datalinkmodel link = new datalinkmodel("viewQSDonVi");
            PagedViewModel<QuanNhan>.currencegroup = 0;
            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;




            return RedirectToAction("ViewDaiDoi", "DaiDoi");

        }


        public IActionResult linkviewAddInf()
        {

            datalinkmodel link = new datalinkmodel("viewAddInf");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return View("ViewDaiDoi");

        }



        public IActionResult linkviewForAnalyst()
        {

            datalinkmodel link = new datalinkmodel("viewForAnalyst");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return View("ViewDaiDoi");

        }



        public IActionResult linkviewBaoCao()
        {

            datalinkmodel link = new datalinkmodel("viewBaoCao");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return View("ViewDaiDoi");

        }
    }
}
