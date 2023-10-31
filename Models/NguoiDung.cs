using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class NguoiDung
{
    public string? MaDonVi { get; set; }

    public string? MaChucVu { get; set; }

    public string? MaQuanNhan { get; set; }

    public virtual ChucVu? MaChucVuNavigation { get; set; }

    public virtual DonVi? MaDonViNavigation { get; set; }
}
