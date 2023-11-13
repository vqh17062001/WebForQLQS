using WebForQLQS.Data;

namespace WebForQLQS.Models
{
    public class objforanalyst
    {
        public DateTime? Ngayvang {  get; set; }
        public int TongQS { get; set; }
        public int QSvang { get; set; }
        public float percentage {  get; set; }
        
        private List<DonVi> listdonvi = new List<DonVi>();
        public objforanalyst( DateTime? date) {


            HtqlqsContext _context = new HtqlqsContext();
            
            var listLS = _context.LsQsVangs.Where(x=>x.NgayVang==date).Count();

            Ngayvang = date;
            TongQS=_context.QuanNhans.Count();
            QSvang = listLS;

            percentage = (float)QSvang*100/TongQS;




        }

        public objforanalyst(DateTime? date, string madonvi)
        {


            HtqlqsContext _context = new HtqlqsContext();
            string tendonvi = null;
            listdonvi =_context.DonVis.ToList();
            foreach (var item in listdonvi) {
                if (item.MaDonVi == madonvi) { 
                    tendonvi=item.TenDonVi; break;
                }
            
            }
          
            var listLS = _context.LsQsVangs.Where(x => x.NgayVang == date).Where(x=>x.TenDonVi==tendonvi).Count();

            Ngayvang = date;
            TongQS = _context.QuannhanDonvis.Where(x=>x.MaDonVi==madonvi).Count();
            QSvang = listLS;

            percentage = (float)QSvang * 100 / TongQS;




        }

    }
}
