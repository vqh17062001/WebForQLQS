using Microsoft.AspNetCore.Mvc;
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


            ///////////// khối truyền thông tin chức vụ 
            var tt_qncv = _context.QuannhanChucvus.ToList();

            foreach (var item in tt_qncv) {
                if (item.MaQuanNhan == tt_quannhan.MaQuanNhan)
                {
                    ViewData["TT_QNCV"]=item.MaChucVu;
                }
            
            }
            var tt_chucvu = _context.ChucVus.ToList();
            ViewData["TTChucVu"] = tt_chucvu;

            ///////// khối truyền thông tin đơn vị 

            var tt_qndv = _context.QuannhanDonvis.ToList();

            foreach (var item in tt_qndv)
            {
                if (item.MaQuanNhan == tt_quannhan.MaQuanNhan)
                {
                    ViewData["TT_QNDV"] = item.MaDonVi;
                }

            }
            var tt_donvi = _context.DonVis.ToList();
            ViewData["TTDonVi"] = tt_donvi;


            ///////// khối này truyền thông tin loại quân nhân 


            ViewData["QN_L"] = tt_quannhan.LoaiQn;
            
            var tt_lqn=_context.LoaiQuanNhans.ToList();
            ViewData["TT_LQN"] = tt_lqn;
            //////////// khối này truyền thông tin cấp bậc
            ///
            ViewData["TT_Capbac"] = tt_quannhan.CapBac;

            ////// khối này truyền tên quân nhân 
            ///

            ViewData["TT_tenQN"] = tt_quannhan.HoTen;
            ViewData["TT_maQN"] = tt_quannhan.MaQuanNhan;
            return View();

        }

        public IActionResult changeQNd_buton_click(string id, string input_tenQN, string input_capbac, string select_chucvu, string select_donvi, string select_loaiQN) {

            var recoreQUANNHAN = _context.QuanNhans.FirstOrDefault(c=>c.MaQuanNhan.Equals(id));

            if (recoreQUANNHAN != null) {

                recoreQUANNHAN.HoTen = input_tenQN;
                recoreQUANNHAN.CapBac = input_capbac;
                recoreQUANNHAN.LoaiQn = select_loaiQN;
                _context.SaveChanges();
            
            }
            ////// xoa bang ghi o QN_DV
            var recordtodelete = _context.QuannhanDonvis.FirstOrDefault(it => it.MaQuanNhan== id );
            _context.QuannhanDonvis.Remove(recordtodelete);
            _context.SaveChanges();

            ////////// thêm vào bảng QN_DV 
            var recordQUANNHANDONVI = new QuannhanDonvi()
            {
                MaDonVi = select_donvi,
                MaQuanNhan = id,
                NgayBatDau= DateTime.Now
            };

            _context.QuannhanDonvis.Add(recordQUANNHANDONVI);
            _context.SaveChanges();


            ////// xoa bang ghi QN_CV
            ///

            var recordtodelete1 = _context.QuannhanChucvus.FirstOrDefault(it => it.MaQuanNhan == id);
            _context.QuannhanChucvus.Remove(recordtodelete1);
            _context.SaveChanges();


            //////////// thhêm vào bản QN_CV 
            var recordQUANNHANCHUCVU = new QuannhanChucvu() { 
                
                MaQuanNhan = id,
                MaChucVu=select_chucvu,
                NgayBatDau = DateTime.Now   
                
            
            };

            _context.QuannhanChucvus.Add(recordQUANNHANCHUCVU);
            _context.SaveChanges();




            return RedirectToAction("viewTieuDoan", "TieuDoan");
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
