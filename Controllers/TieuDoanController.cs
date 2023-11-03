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
            var qn_dvlist = _context.QuannhanDonvis.ToList();
            var qn_cvlist = _context.QuannhanChucvus.ToList();

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

                    foreach (var qn1 in qn_dvlist)
                    {

                        if (qn1.MaDonVi == searchvalue && qn1.MaQuanNhan == qn.MaQuanNhan)
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


            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);

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

        /// <summary>
        /// vùng cho phần THÊM
        /// </summary>
        /// <returns></returns>
        public IActionResult linkviewAddInfd()
        {

            datalinkmodel link = new datalinkmodel("viewAddInfd");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);
            ViewData["name"] = ten_nguoi_dangnhap.HoTen;


            ViewData["TT_ChuVu"] = _context.ChucVus.ToList();
            ViewData["TT_DonVi"] = _context.DonVis.ToList();
            ViewData["TT_LoaiQN"] = _context.LoaiQuanNhans.ToList();
            return View("ViewTieuDoan");

        }



        public IActionResult AddQNd_buton_click(string input_tenQN, string input_capbac, string select_chucvu, string select_donvi, string select_loaiQN)
        {

            if (input_tenQN != null)
            {

                var recordtoaddQUANNHAN = new QuanNhan()
                {
                    MaQuanNhan = "trust",
                    HoTen = input_tenQN,
                    CapBac = input_capbac,
                    LoaiQn = select_loaiQN,

                };
                _context.QuanNhans.Add(recordtoaddQUANNHAN);
                _context.SaveChanges();


                string maQN = null;

                var temp = _context.QuannhanDonvis.ToList();
                var temp2 = _context.QuanNhans.ToList();

                foreach (var d in temp2)
                {
                    foreach (var c in temp)
                    {
                        if (d.MaQuanNhan == c.MaQuanNhan)
                        {
                            maQN = null;
                            continue;
                        }
                        else { maQN = d.MaQuanNhan; }
                    }
                }






                var maQN_moithem = _context.QuanNhans.Find(maQN);


                var recordtoaddQN_CV = new QuannhanChucvu()
                {
                    MaQuanNhan = maQN_moithem.MaQuanNhan,
                    MaChucVu = select_chucvu,
                    NgayBatDau = DateTime.Now

                };

                _context.QuannhanChucvus.Add(recordtoaddQN_CV);
                _context.SaveChanges();

                var recordtoaddQN_DV = new QuannhanDonvi()
                {
                    MaQuanNhan = maQN_moithem.MaQuanNhan,
                    MaDonVi = select_donvi,

                    NgayBatDau = DateTime.Now
                };

                _context.QuannhanDonvis.Add(recordtoaddQN_DV);
                _context.SaveChanges();

                return RedirectToAction("viewTieuDoan", "TieuDoan");
            }
            else
            {

                return RedirectToAction("viewTieuDoan", "TieuDoan");


            }




        }

        /// <summary>
        /// vùng cho THỐNG KÊ 
        /// </summary>
        /// <returns></returns>


        public IActionResult linkviewForAnalystd()
        {

            datalinkmodel link = new datalinkmodel("viewForAnalystd");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return View("ViewTieuDoan");

        }

        /// <summary>
        /// vùng cho XÁT NHẬN VẮNG 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>

        public IActionResult linkviewBaoCaod(int page = 1)
        {
            datalinkmodel link = new datalinkmodel("viewBaoCaod");
            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idten);
            ViewData["name"] = ten_nguoi_dangnhap.HoTen;


            //////////////  vùng đưa thông tin 
            ///

            int pageSize = 10;

            var baocaoqsngaylist = _context.BaoCaoQsNgays.ToList();

            var pagedItems = baocaoqsngaylist.Skip((page - 1) * pageSize).Take(pageSize);

            var model = new PagedViewModel<BaoCaoQsNgay>
            {
                Items = pagedItems.ToList(),
                TotalItems = baocaoqsngaylist.Count,
                CurrentPage = page,
                PageSize = pageSize

            };
            ////////// vung xử lý tiềm kiếm 
            ///
            var item = TempData["idsearch"] as string;
            if (item != null)
            {
                searchvalue = item;

                var quannhanlist = _context.QuanNhans.ToList();
                foreach (var qn in quannhanlist)
                {
                    if (qn.HoTen.Contains(searchvalue))
                    {

                        searchvalue = qn.MaQuanNhan; break;
                    }

                }


            }
            if (item == "all" || item == "tất cả")
            {
                searchvalue = null;

            }


            List<BaoCaoQsNgay> pageitemsearch = new List<BaoCaoQsNgay>();

            if (searchvalue != null)
            {

                foreach (var bc in baocaoqsngaylist)
                {

                    if (bc.MaBc.Contains(searchvalue) || bc.MaQuanNhan == searchvalue)
                    {

                        pageitemsearch.Add(bc);

                    }



                }


                pagedItems = pageitemsearch.Skip((page - 1) * pageSize).Take(pageSize);

                model.Items = pagedItems.ToList();
                model.TotalItems = pageitemsearch.Count;
                model.CurrentPage = page;
                model.PageSize = pageSize;



            }
            ////////// 
            ///

            ViewData["TT_ChucVU"] = _context.QuannhanChucvus.ToList();
            ViewData["TT_DonVi"] = _context.QuannhanDonvis.ToList();
            ViewData["TT_QuanNhan"] = _context.QuanNhans.ToList();
            return View("ViewTieuDoan", model);

        }

        public IActionResult DeleteRecordBaoCao(string id)
        {

            var recordtodelete = _context.BaoCaoQsNgays.Find(id);
            if (recordtodelete != null)
            {
                _context.BaoCaoQsNgays.Remove(recordtodelete);
                _context.SaveChanges();
            }


            return RedirectToAction("linkviewBaoCaod", "TieuDoan");

        }






        /// <summary>
        /// vùng các chức năng của DANH SÁCH QUÂN NHÂN TIỂU ĐOÀN 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult changeQNd(string id)
        {
            var tt_quannhan = _context.QuanNhans.Find(id);


            ///////////// khối truyền thông tin chức vụ 
            var tt_qncv = _context.QuannhanChucvus.ToList();

            foreach (var item in tt_qncv)
            {
                if (item.MaQuanNhan == tt_quannhan.MaQuanNhan)
                {
                    ViewData["TT_QNCV"] = item.MaChucVu;
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

            var tt_lqn = _context.LoaiQuanNhans.ToList();
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

        public IActionResult changeQNd_buton_click(string id, string input_tenQN, string input_capbac, string select_chucvu, string select_donvi, string select_loaiQN)
        {

            var recoreQUANNHAN = _context.QuanNhans.FirstOrDefault(c => c.MaQuanNhan.Equals(id));

            if (recoreQUANNHAN != null)
            {

                recoreQUANNHAN.HoTen = input_tenQN;
                recoreQUANNHAN.CapBac = input_capbac;
                recoreQUANNHAN.LoaiQn = select_loaiQN;
                _context.SaveChanges();

            }
            ////// xoa bang ghi o QN_DV
            var recordtodelete = _context.QuannhanDonvis.FirstOrDefault(it => it.MaQuanNhan == id);
            _context.QuannhanDonvis.Remove(recordtodelete);
            _context.SaveChanges();

            ////////// thêm vào bảng QN_DV 
            var recordQUANNHANDONVI = new QuannhanDonvi()
            {
                MaDonVi = select_donvi,
                MaQuanNhan = id,
                NgayBatDau = DateTime.Now
            };

            _context.QuannhanDonvis.Add(recordQUANNHANDONVI);
            _context.SaveChanges();


            ////// xoa bang ghi QN_CV
            ///

            var recordtodelete1 = _context.QuannhanChucvus.FirstOrDefault(it => it.MaQuanNhan == id);
            _context.QuannhanChucvus.Remove(recordtodelete1);
            _context.SaveChanges();


            //////////// thhêm vào bản QN_CV 
            var recordQUANNHANCHUCVU = new QuannhanChucvu()
            {

                MaQuanNhan = id,
                MaChucVu = select_chucvu,
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

        public IActionResult table_searchButtonClickinXATNHANVANG(string table_search)
        {


            TempData["idsearch"] = table_search;
            return RedirectToAction("linkviewBaoCaod", "TieuDoan");
        }
    }
}
