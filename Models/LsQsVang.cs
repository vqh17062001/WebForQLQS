using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class LsQsVang
{
    public string MaLs { get; set; } = null!;

    public string? MaQuanNhan { get; set; }

    public string? HoTen { get; set; }

    public string? CapBac { get; set; }

    public string? ChucVu { get; set; }

    public string? TenDonVi { get; set; }

    public string? LyDo { get; set; }

    public DateTime? NgayVang { get; set; }

    public DateTime? NgayDuyet { get; set; }

    public string? NguoiDuyet { get; set; }

    public virtual ChucVu? ChucVuNavigation { get; set; }

    public virtual LyDo? LyDoNavigation { get; set; }
}
