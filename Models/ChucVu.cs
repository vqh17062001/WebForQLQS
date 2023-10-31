using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class ChucVu
{
    public string MaChucVu { get; set; } = null!;

    public string? TenChucVu { get; set; }

    public virtual ICollection<LsQsVang> LsQsVangs { get; set; } = new List<LsQsVang>();

    public virtual ICollection<QuannhanChucvu> QuannhanChucvus { get; set; } = new List<QuannhanChucvu>();
}
