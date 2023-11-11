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
        static string idtenindaidoi;
        static string searchvalueindaidoi;


        public IActionResult ViewDaiDoi(int page = 1)
        {
            //////////////
            ///
            datalinkmodel link = new datalinkmodel("viewQSDonVi");

            ViewBag.linkmodel = link;
            if (TempData["name"] != null)
            {

                idtenindaidoi = TempData["name"] as string;
            }
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idtenindaidoi);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            ///////////
            ///
            string madonvi = null;
            var qn_dvlist = _context.QuannhanDonvis.ToList();
            foreach (var qndv in qn_dvlist)
            {
                if (qndv.MaQuanNhan == idtenindaidoi)
                {
                    madonvi = qndv.MaDonVi;
                    break;
                }

            }

            List<string> listmaquannhan = new List<string>();

            foreach (var qndv in qn_dvlist)
            {

                if (qndv.MaDonVi == madonvi)
                {
                    listmaquannhan.Add(qndv.MaQuanNhan);
                }

            }
            var quannhanlistfull = _context.QuanNhans.ToList();
            List<QuanNhan> quannhandaidoilist = new List<QuanNhan>();
            foreach (var maqn in listmaquannhan)
            {

                foreach (var qn in quannhanlistfull)
                {
                    if (qn.MaQuanNhan == maqn)
                    {
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






            ///////////
            ///

            var item = TempData["idsearch"] as string;

            if (item != null)
            {
                searchvalueindaidoi = item;

            }
            List<QuanNhan> pageitemsearch = new List<QuanNhan>();

            if (searchvalueindaidoi != null)
            {
                foreach (var qn in quannhandaidoilist)


                {



                    if (qn.CapBac.Contains(searchvalueindaidoi) || qn.HoTen.Contains(searchvalueindaidoi))
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



        public IActionResult linkviewQSDonVi()
        {
            searchvalueindaidoi = null;
            datalinkmodel link = new datalinkmodel("viewQSDonVi");
            PagedViewModel<QuanNhan>.currencegroup = 0;
            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idtenindaidoi);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;




            return RedirectToAction("ViewDaiDoi", "DaiDoi");

        }


        public IActionResult linkviewAddInf()
        {

            datalinkmodel link = new datalinkmodel("viewAddInf");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idtenindaidoi);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return View("ViewDaiDoi");

        }



        public IActionResult linkviewForAnalyst()
        {

            datalinkmodel link = new datalinkmodel("viewForAnalyst");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idtenindaidoi);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return View("ViewDaiDoi");

        }



        public IActionResult linkviewBaoCao()
        {

            datalinkmodel link = new datalinkmodel("viewBaoCao");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idtenindaidoi);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return View("ViewDaiDoi");

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="table_search"></param>
        /// <returns></returns>

        public IActionResult DeleteQNd(string id)
        {

            using (var context = new HtqlqsContext()) // Thay YourDbContext bằng tên của DbContext của bạn
            {
                var recordToDelete = context.QuanNhans.Find(id);
                if (recordToDelete != null)
                {

                    try
                    {
                        context.QuanNhans.Remove(recordToDelete);
                        context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        TempData["mess"] = "Quân nhân đang trong danh sách báo vắng!";
                        return RedirectToAction("ViewDaiDoi", "DaiDoi");

                    }
                }
            }

            TempData["mess"] = "Xóa thành công! ";
            // var quannhan = _context.QuanNhans.ExecuteDelete(id);
            return RedirectToAction("ViewDaiDoi", "DaiDoi");


        }




        public IActionResult BaovangQNd(string id)
        {
            //var bcqsngay=_context.BaoCaoQsNgays.ToList();
            using (var context = new HtqlqsContext())
            {
                var bcqsngay = context.BaoCaoQsNgays.ToList();
                var recordToAddBCQSNgay = context.QuanNhans.Find(id);
                var idNguoiduyet = context.QuanNhans.Find(idtenindaidoi);


                var recordBaoCao = new BaoCaoQsNgay();


                recordBaoCao.MaBc = "trust";
                recordBaoCao.MaQuanNhan = recordToAddBCQSNgay.MaQuanNhan;
                recordBaoCao.NgayVang = DateTime.Now.Date;
                recordBaoCao.NguoiDuyet = idNguoiduyet.MaQuanNhan;

                foreach (var record in bcqsngay)
                {

                    if (record.MaQuanNhan == recordBaoCao.MaQuanNhan && record.NgayVang == recordBaoCao.NgayVang)
                    {

                        TempData["mess"] = "Quân nhân đã được báo vắng trong ngày!";
                        return RedirectToAction("ViewDaiDoi", "DaiDoi");

                    }

                }

                context.BaoCaoQsNgays.Add(recordBaoCao);
                context.SaveChanges();
            }


            return RedirectToAction("ViewDaiDoi", "DaiDoi");

        }






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
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idtenindaidoi);
            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            return View();

        }



        public IActionResult changeQNd_buton_click(string id, string input_tenQN, string input_capbac, string select_chucvu, string select_donvi, string select_loaiQN)
        {

            var recoreQUANNHAN = _context.QuanNhans.FirstOrDefault(c => c.MaQuanNhan.Equals(id));

            if (recoreQUANNHAN != null && input_tenQN != null)
            {

                recoreQUANNHAN.HoTen = input_tenQN;
                recoreQUANNHAN.CapBac = input_capbac;
                recoreQUANNHAN.LoaiQn = select_loaiQN;
                _context.SaveChanges();

            }
            else
            {
                TempData["mess"] = "Sửa thất bại!";
                return RedirectToAction("ViewDaiDoi", "DaiDoi");

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



            TempData["mess"] = "Sửa thành công!";
            return RedirectToAction("ViewDaiDoi", "DaiDoi");
        }




















        [HttpPost]

        public IActionResult table_searchButtonClick(string table_search)
        {




            TempData["idsearch"] = table_search;
            return RedirectToAction("ViewDaiDoi", "DaiDoi");
        }


    }
}
