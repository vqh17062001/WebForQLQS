﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using WebForQLQS.Data;
using WebForQLQS.Models;

//using System.Collections.Generic;

namespace WebForQLQS.Controllers
{
    public class TieuDoanController : Controller
    {

        private HtqlqsContext _context = new HtqlqsContext();

        static string idten;
        static string searchvalue;




        public IActionResult viewTieuDoan(int page = 1)


        {

            int pageSize = 10;



            var quannhanlist = _context.QuanNhans.ToList();
            var qn_dvlist=_context.QuannhanDonvis.ToList();
            var qn_cvlist=_context.QuannhanChucvus.ToList();

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

            if (searchvalue != null)
            {
                foreach (var qn in quannhanlist)


                {

                    foreach (var qn1 in qn_dvlist) { 
                    
                        if (qn1.MaDonVi == searchvalue && qn1.MaQuanNhan==qn.MaQuanNhan )
                        {
                            pageitemsearch.Add(qn);
                        }

                    
                    }

                    if (qn.CapBac.Contains(searchvalue) || qn.HoTen.Contains(searchvalue))
                    {
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
            if (TempData["name"] != null)
            {

                idten = TempData["name"] as string;
            }
            

            var ten_nguoi_dangnhap=_context.QuanNhans.Find(idten);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;

            ViewData["ttDonVi"] = qn_dvlist;
            ViewData["ttChucVu"] = qn_cvlist;

            return View(model);

        }



        [HttpGet]



        public IActionResult linkviewQSDonVid()
        {

            searchvalue = null;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return RedirectToAction("viewTieuDoan", "TieuDoan");

        }


        public IActionResult linkviewAddInfd()
        {

            datalinkmodel link = new datalinkmodel("viewAddInfd");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return View("ViewTieuDoan");

        }



        public IActionResult linkviewForAnalystd()
        {

            datalinkmodel link = new datalinkmodel("viewForAnalystd");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return View("ViewTieuDoan");

        }



        public IActionResult linkviewBaoCaod()
        {

            datalinkmodel link = new datalinkmodel("viewBaoCaod");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return View("ViewTieuDoan");

        }


        public IActionResult changeQNd(string id)
        {
            var tt_quannhan = _context.QuanNhans.Find(id);
            var tt_qncv = _context.QuannhanChucvus.ToList();
            foreach (var item in tt_qncv) {
                if (item.MaQuanNhan == tt_quannhan.MaQuanNhan)
                {
                    ViewData["TT_QNCV"]=item.MaChucVu;
                }
            
            }
            var tt_chucvu = _context.ChucVus.ToList();

            
            ViewData["TTChucVu"] = tt_chucvu;
            return View();

        }



        public IActionResult DeleteQNd(string id)
        {

            using (var context = new HtqlqsContext()) // Thay YourDbContext bằng tên của DbContext của bạn
            {
                var recordToDelete = context.QuanNhans.Find(id);
                if (recordToDelete != null)
                {
                    context.QuanNhans.Remove(recordToDelete);
                    context.SaveChanges();
                }
            }


            // var quannhan = _context.QuanNhans.ExecuteDelete(id);
            return RedirectToAction("viewTieuDoan", "TieuDoan");

        }



        public IActionResult BaovangQNd(string id)
        {

            using (var context = new HtqlqsContext())
            {
                var recordToAddBCQSNgay = context.QuanNhans.Find(id);
                var idNguoiduyet = context.QuanNhans.Find(idten);


                var recordBaoCao = new BaoCaoQsNgay();
                recordBaoCao.MaBc = "trust";
                recordBaoCao.MaQuanNhan = recordToAddBCQSNgay.MaQuanNhan;
                recordBaoCao.NgayVang = DateTime.Now;
                recordBaoCao.NguoiDuyet = idNguoiduyet.MaQuanNhan;



                context.BaoCaoQsNgays.Add(recordBaoCao);
                context.SaveChanges();
            }


            return RedirectToAction("viewTieuDoan", "TieuDoan");

        }




        [HttpPost]
        public IActionResult table_searchButtonClick(string table_search)
        {




            TempData["idsearch"] = table_search;
            return RedirectToAction("viewTieuDoan", "TieuDoan");
        }


    }
}
