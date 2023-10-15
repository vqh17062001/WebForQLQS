using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class QuanNhan
{
    public string MaQuanNhan { get; set; } = null!;

    public string? HoTen { get; set; }

    public string? CapBac { get; set; }

    public string? ChucVu { get; set; }

    public string? DonVi { get; set; }

    public string? LoaiQn { get; set; }

    public virtual ICollection<BaoCaoQsNgay> BaoCaoQsNgayMaQuanNhanNavigations { get; set; } = new List<BaoCaoQsNgay>();

    public virtual ICollection<BaoCaoQsNgay> BaoCaoQsNgayNguoiDuyetNavigations { get; set; } = new List<BaoCaoQsNgay>();

    public virtual ChucVu? ChucVuNavigation { get; set; }

    public virtual DonVi? DonViNavigation { get; set; }

    public virtual LoaiQuanNhan? LoaiQnNavigation { get; set; }

    public virtual ICollection<LsQsVang> LsQsVangs { get; set; } = new List<LsQsVang>();

    public virtual ICollection<ThongBaoTrongNgay> ThongBaoTrongNgays { get; set; } = new List<ThongBaoTrongNgay>();
}
