using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class QuannhanChucvu
{
    public string MaQuanNhan { get; set; } = null!;

    public string MaChucVu { get; set; } = null!;

    public DateTime? NgayBatDau { get; set; }

    public DateTime? NgayKetThuc { get; set; }

    public virtual ChucVu MaChucVuNavigation { get; set; } = null!;

    public virtual QuanNhan MaQuanNhanNavigation { get; set; } = null!;
}
