using WebForQLQS.Data;

namespace WebForQLQS.Models
{
    public class objforanalyst
    {
        public DateTime? Ngayvang {  get; set; }
        public int TongQS { get; set; }
        public int QSvang { get; set; }
        public float percentage {  get; set; }
        
        
        public objforanalyst( DateTime? date) {


            HtqlqsContext _context = new HtqlqsContext();
            
            var listLS = _context.LsQsVangs.Where(x=>x.NgayVang==date).Count();

            Ngayvang = date;
            TongQS=_context.QuanNhans.Count();
            QSvang = listLS;

            percentage = (float)QSvang*100/TongQS;




        }

    }
}
