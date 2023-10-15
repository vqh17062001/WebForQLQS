using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class BaoCaoQsNgay
{
    public string MaBc { get; set; } = null!;

    public string? MaQuanNhan { get; set; }

    public DateTime? NgayVang { get; set; }

    public string? LyDo { get; set; }

    public string? NguoiDuyet { get; set; }

    public string? ChiTiet { get; set; }

    public virtual LyDo? LyDoNavigation { get; set; }

    public virtual QuanNhan? MaQuanNhanNavigation { get; set; }

    public virtual QuanNhan? NguoiDuyetNavigation { get; set; }
}
