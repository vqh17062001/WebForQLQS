using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class DonVi
{
    public string MaDonVi { get; set; } = null!;

    public string? TenDonVi { get; set; }

    public virtual ICollection<QuanNhan> QuanNhans { get; set; } = new List<QuanNhan>();

    public virtual ICollection<ThongBaoTrongNgay> ThongBaoTrongNgays { get; set; } = new List<ThongBaoTrongNgay>();
}
