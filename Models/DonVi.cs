using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class DonVi
{
    public string MaDonVi { get; set; } = null!;

    public string? TenDonVi { get; set; }

    public virtual ICollection<QuannhanDonvi> QuannhanDonvis { get; set; } = new List<QuannhanDonvi>();

    public virtual ICollection<ThongBaoTrongNgay> ThongBaoTrongNgays { get; set; } = new List<ThongBaoTrongNgay>();
}
