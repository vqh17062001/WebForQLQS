using System;
using System.Collections.Generic;

namespace WebForQLQS.Models;

public partial class LyDo
{
    public string MaLd { get; set; } = null!;

    public string? LoaiLd { get; set; }

    public virtual ICollection<BaoCaoQsNgay> BaoCaoQsNgays { get; set; } = new List<BaoCaoQsNgay>();

    public virtual ICollection<LsQsVang> LsQsVangs { get; set; } = new List<LsQsVang>();
}
