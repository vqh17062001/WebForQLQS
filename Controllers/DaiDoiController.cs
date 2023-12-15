using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
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




            ViewData["ativeinviewDaiDoi"] = "active";
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

        /// <summary>
        ///  vung cho THEM
        /// </summary>
        /// <returns></returns>
        public IActionResult linkviewAddInf()
        {

            datalinkmodel link = new datalinkmodel("viewAddInf");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idtenindaidoi);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;

            ViewData["TT_ChuVu"] = _context.ChucVus.ToList();
            ViewData["TT_DonVi"] = _context.DonVis.ToList();
            ViewData["TT_LoaiQN"] = _context.LoaiQuanNhans.ToList();
            ViewData["currencedonvi"] = _context.QuannhanDonvis.Where(x => x.MaQuanNhan == idtenindaidoi).FirstOrDefault();

            ViewData["ativeinlinkviewAddInf"] = "active";
            return View("ViewDaiDoi");

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
                TempData["mess"] = $"Đã thêm quân nhân {input_tenQN} thành công!";
                return RedirectToAction("ViewDaiDoi", "DaiDoi");
            }
            else
            {
                TempData["mess"] = "Thêm thông tin thất bại!";
                return RedirectToAction("ViewDaiDoi", "DaiDoi");


            }




        }






        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>


        public IActionResult linkviewForAnalyst(int page = 1)
        {

            datalinkmodel link = new datalinkmodel("viewForAnalyst");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idtenindaidoi);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;


            ////////
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


            //////

            int pageSize = 10;
            var objforanalystlist = new List<objforanalyst>();
            var listLS = _context.LsQsVangs.ToList();
            var listday = listLS.Select(x => x.NgayVang).Distinct().OrderByDescending(x => x).ToList();

            foreach (var item in listday)
            {

                var recordobj = new objforanalyst(item, madonvi);
                objforanalystlist.Add(recordobj);

            }
            var pagedItems = objforanalystlist.Skip((page - 1) * pageSize).Take(pageSize);

            var model = new PagedViewModel<objforanalyst>
            {
                Items = pagedItems.ToList(),
                TotalItems = objforanalystlist.Count,
                CurrentPage = page,
                PageSize = pageSize

            };
            /////////////////
            ///  vung timf kiem
            if (TempData["idsearchday"] != null)
            {
                DateTime x;
                try
                {
                    x = DateTime.Parse(TempData["idsearchday"] as string);
                }
                catch
                {
                    TempData["idsearchday"] = null;
                    TempData["mess"] = "Ngày không hợp lệ!";
                    return RedirectToAction("linkviewForAnalyst", "DaiDoi");
                }

                var recordobj1 = new objforanalyst(x, madonvi);
                objforanalystlist.Clear();
                objforanalystlist.Add(recordobj1);

                pagedItems = objforanalystlist.Skip((page - 1) * pageSize).Take(pageSize);

                model = new PagedViewModel<objforanalyst>
                {
                    Items = pagedItems.ToList(),
                    TotalItems = objforanalystlist.Count,
                    CurrentPage = page,
                    PageSize = pageSize

                };
            }

            //List<objforanalyst> 
            ViewData["mess"] = TempData["mess"];
            ViewData["phienhieu"] = madonvi;



            ViewData["ativeinlinkviewForAnalyst"] = "active";
            return View("ViewDaiDoi", model);







        }


        static string searchvalueindetailforanalyst;
        static DateTime? globledate;
        public IActionResult detailforanalyst(DateTime? date, int page = 1)
        {
            if (page == 1)
            {
                searchvalueindetailforanalyst = null;
            }
            if (date != null)
            {
                globledate = date;
            }
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idtenindaidoi);

            //string sqlcomand = $"select top(1) Ten_don_vi from DON_VI, QUANNHAN_DONVI where DON_VI.Ma_Don_vi=QUANNHAN_DONVI.Ma_Don_vi and QUANNHAN_DONVI.Ma_Quan_nhan='{ten_nguoi_dangnhap.MaQuanNhan}'";

            string madonvi = _context.QuannhanDonvis.Where(x => x.MaQuanNhan == ten_nguoi_dangnhap.MaQuanNhan).FirstOrDefault()?.MaDonVi;
            string tendonvi = _context.DonVis.Where(x => x.MaDonVi == madonvi).FirstOrDefault()?.TenDonVi;

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            ViewData["currencedate"] = globledate;
            ////////
            ///
            var recordngaylist = _context.LsQsVangs.Where(x => x.NgayVang == globledate).Where(x => x.TenDonVi == tendonvi).ToList();

            var lydo = _context.LyDos.ToList();


            ViewData["lydo"] = lydo;


            /////
            ///
            int pageSize = 10;
            var pagedItems = recordngaylist.Skip((page - 1) * pageSize).Take(pageSize);

            var model = new PagedViewModel<LsQsVang>
            {
                Items = pagedItems.ToList(),
                TotalItems = recordngaylist.Count,
                CurrentPage = page,
                PageSize = pageSize

            };
            //////
            /// vung tim kiem
            /// 
            var item = TempData["idsearch"] as string;
            if (item != null)
            {
                searchvalueindetailforanalyst = item;
            }

            if (searchvalueindetailforanalyst != null)
            {
                List<LsQsVang> otherrecordLSlist = new List<LsQsVang>();
                string searchlydo = null;
                foreach (var y in lydo)
                {
                    if (y.LoaiLd.Contains(searchvalueindetailforanalyst))
                    {

                        searchlydo = y.MaLd;
                    }
                }


                //searchvalueindetailforanalyst = item;
                foreach (var x in recordngaylist)
                {
                    if (x.HoTen.Contains(searchvalueindetailforanalyst) || x.TenDonVi.Contains(searchvalueindetailforanalyst) || x.LyDo == searchlydo)
                    {

                        otherrecordLSlist.Add(x);
                    }

                }
                pagedItems = otherrecordLSlist.Skip((page - 1) * pageSize).Take(pageSize);
                model.Items = pagedItems.ToList();
                model.TotalItems = otherrecordLSlist.Count;
                model.CurrentPage = page;
                model.PageSize = pageSize;
            }

            return View(model);
        }


        /// <summary>
        /// vungf cho baos vawngs
        ///
        /// </summary>
        /// <returns></returns>

        public IActionResult linkviewBaoCao(int page = 1)
        {

            datalinkmodel link = new datalinkmodel("viewBaoCao");

            ViewBag.linkmodel = link;
            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idtenindaidoi);
            var donvi = _context.QuannhanDonvis.Where(x => x.MaQuanNhan == idtenindaidoi).FirstOrDefault();
            ViewData["name"] = ten_nguoi_dangnhap.HoTen;
            /////
            ///

            //////////////  vùng đưa thông tin 
            ///

            int pageSize = 10;

            var baocaoqsngaylist = _context.BaoCaoQsNgays.Where(x => x.MaBc.Contains(donvi.MaDonVi)).ToList();

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
                searchvalueindaidoi = item;

                var quannhanlist = _context.QuanNhans.ToList();
                foreach (var qn in quannhanlist)
                {
                    if (qn.HoTen.Contains(searchvalueindaidoi))
                    {

                        searchvalueindaidoi = qn.MaQuanNhan; break;
                    }

                }


            }
            if (item == "all" || item == "tất cả")
            {
                searchvalueindaidoi = null;

            }


            List<BaoCaoQsNgay> pageitemsearch = new List<BaoCaoQsNgay>();

            if (searchvalueindaidoi != null)
            {

                foreach (var bc in baocaoqsngaylist)
                {

                    if (bc.MaBc.Contains(searchvalueindaidoi) || bc.MaQuanNhan == searchvalueindaidoi)
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
            ViewData["TT_Lydo"] = _context.LyDos.ToList();
            ViewData["mess"] = TempData["mess"];



            ViewData["ativeinlinkviewBaoCaod"] = "active";
            return View("ViewDaiDoi", model);








        }

        public IActionResult DeleteRecordBaoCao(string id)
        {

            var recordtodelete = _context.BaoCaoQsNgays.Find(id);
            if (recordtodelete != null)
            {
                _context.BaoCaoQsNgays.Remove(recordtodelete);
                _context.SaveChanges();
            }


            return RedirectToAction("linkviewBaoCao", "DaiDoi");

        }


        public IActionResult detailRecordBaoCao(string id)
        {

            var ten_nguoi_dangnhap = _context.QuanNhans.Find(idtenindaidoi);

            ViewData["name"] = ten_nguoi_dangnhap.HoTen;

            var ttrecordvang = _context.BaoCaoQsNgays.Find(id);

            var ttQuanNhan = _context.QuanNhans.ToList();
            //////// ho ten
            foreach (var qn in ttQuanNhan)
            {

                if (qn.MaQuanNhan == ttrecordvang.MaQuanNhan)
                {
                    ViewData["tenquannhan"] = qn.HoTen;
                }
            }
            ///////  cap bac
            foreach (var qn in ttQuanNhan)
            {

                if (qn.MaQuanNhan == ttrecordvang.MaQuanNhan)
                {
                    ViewData["capbacquannhan"] = qn.CapBac;
                }
            }
            ///////  chuc vu

            var listQN_CV = _context.QuannhanChucvus.ToList();

            foreach (var qncv in listQN_CV)
            {

                if (qncv.MaQuanNhan == ttrecordvang.MaQuanNhan)
                {

                    var chucvu = _context.ChucVus.Find(qncv.MaChucVu);
                    ViewData["tenchucvu"] = chucvu.TenChucVu;

                }

            }

            ///////  don vi

            var listQN_DV = _context.QuannhanDonvis.ToList();

            foreach (var qndv in listQN_DV)
            {

                if (qndv.MaQuanNhan == ttrecordvang.MaQuanNhan)
                {

                    var donvi = _context.DonVis.Find(qndv.MaDonVi);
                    ViewData["tendonvi"] = donvi.TenDonVi;

                }

            }


            ///////  nguoi bao
            foreach (var qn in ttQuanNhan)
            {

                if (qn.MaQuanNhan == ttrecordvang.NguoiDuyet)
                {
                    ViewData["tennguoiduyet"] = qn.HoTen;
                }
            }
            /////// ly do

            ViewData["lydo"] = _context.LyDos.ToList();

            ///////
            ///
            ViewData["ngayvang"] = ttrecordvang.NgayVang;
            ViewData["maBC"] = ttrecordvang;
            return View();
        }



        public IActionResult ThemchitietBaoCao(string id, string select_lydo, string textarea_chitiet)
        {

            var record = _context.BaoCaoQsNgays.Find(id);
            if (record != null && select_lydo != null)
            {
                record.LyDo = select_lydo;
                record.ChiTiet = textarea_chitiet;
                _context.SaveChanges();
                TempData["mess"] = "Thêm chi tiết thành công!";
                return RedirectToAction("linkviewBaoCao", "DaiDoi");
            }
            return RedirectToAction("linkviewBaoCao", "DaiDoi");

        }

        /// <summary>
        ///  vung cho DANH SACH QUAN NHAN DAO DOI
        /// </summary>
        /// <param name="table_search"></param>
        /// <returns></returns>

        public IActionResult DeleteQNd(string id)
        {

            using (var context = new HtqlqsContext()) // Thay YourDbContext bằng tên của DbContext của bạn
            {
                var recordToDelete = context.QuanNhans.Find(id);
                var otherrecord = context.NguoiDungs.Where(x => x.MaQuanNhan == id);

                if (recordToDelete != null && otherrecord == null)
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
                else
                {

                    TempData["mess"] = "Không được quyền xóa người dùng!";
                    return RedirectToAction("ViewDaiDoi", "DaiDoi");


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

        public IActionResult table_searchButtonClickinXATNHANVANG(string table_search)
        {


            TempData["idsearch"] = table_search;
            return RedirectToAction("linkviewBaoCao", "DaiDoi");
        }

        public IActionResult table_searchButtonClickinTHONGKE(string table_search)
        {


            TempData["idsearchday"] = table_search;
            return RedirectToAction("linkviewForAnalyst", "DaiDoi");
        }

        public IActionResult table_searchButtonClickindetailforanalyst(string table_search)
        {

            TempData["idsearch"] = table_search;
            return RedirectToAction("detailforanalyst", "DaiDoi");

        }

    }
}
