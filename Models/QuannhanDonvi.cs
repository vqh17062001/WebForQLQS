using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class QuannhanDonvi
{
    public string MaQuanNhan { get; set; } = null!;

    public string MaDonVi { get; set; } = null!;

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public virtual DonVi MaDonViNavigation { get; set; } = null!;

    public virtual QuanNhan MaQuanNhanNavigation { get; set; } = null!;
}
