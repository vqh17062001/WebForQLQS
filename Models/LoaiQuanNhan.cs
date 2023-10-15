using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class LoaiQuanNhan
{
    public string MaLoai { get; set; } = null!;

    public string? TenLoai { get; set; }

    public virtual ICollection<QuanNhan> QuanNhans { get; set; } = new List<QuanNhan>();
}
