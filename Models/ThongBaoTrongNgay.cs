using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class ThongBaoTrongNgay
{
    public string MaThongBao { get; set; } = null!;

    public DateTime? NgayGio { get; set; }

    public string? NguoiGui { get; set; }

    public string? DonViNhan { get; set; }

    public string? NoiDung { get; set; }

    public virtual DonVi? DonViNhanNavigation { get; set; }

    public virtual QuanNhan? NguoiGuiNavigation { get; set; }
}
