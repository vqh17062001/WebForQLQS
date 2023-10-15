using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebForQLQS.Models;

namespace WebForQLQS.Data;

public partial class HtqlqsContext : DbContext
{
    public HtqlqsContext()
    {
    }

    public HtqlqsContext(DbContextOptions<HtqlqsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BaoCaoQsNgay> BaoCaoQsNgays { get; set; }

    public virtual DbSet<ChucVu> ChucVus { get; set; }

    public virtual DbSet<DonVi> DonVis { get; set; }

    public virtual DbSet<LoaiQuanNhan> LoaiQuanNhans { get; set; }

    public virtual DbSet<LsQsVang> LsQsVangs { get; set; }

    public virtual DbSet<LyDo> LyDos { get; set; }

    public virtual DbSet<QuanNhan> QuanNhans { get; set; }

    public virtual DbSet<ThongBaoTrongNgay> ThongBaoTrongNgays { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=VO-QUOC-HUY;Initial Catalog=HTQLQS;Persist Security Info=True;User ID=sa;Password=Qnvn16062001@;Integrated Security=True;Encrypt=false;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BaoCaoQsNgay>(entity =>
        {
            entity.HasKey(e => e.MaBc).HasName("PK__BAO_CAO___2E67755A163092BC");

            entity.ToTable("BAO_CAO_QS_NGAY");

            entity.Property(e => e.MaBc)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_BC");
            entity.Property(e => e.ChiTiet)
                .HasMaxLength(100)
                .HasColumnName("Chi_tiet");
            entity.Property(e => e.LyDo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ly_do");
            entity.Property(e => e.MaQuanNhan)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Quan_nhan");
            entity.Property(e => e.NgayVang)
                .HasColumnType("date")
                .HasColumnName("Ngay_vang");
            entity.Property(e => e.NguoiDuyet)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Nguoi_duyet");

            entity.HasOne(d => d.LyDoNavigation).WithMany(p => p.BaoCaoQsNgays)
                .HasForeignKey(d => d.LyDo)
                .HasConstraintName("FK__BAO_CAO_Q__Ly_do__6477ECF3");

            entity.HasOne(d => d.MaQuanNhanNavigation).WithMany(p => p.BaoCaoQsNgayMaQuanNhanNavigations)
                .HasForeignKey(d => d.MaQuanNhan)
                .HasConstraintName("FK__BAO_CAO_Q__Ma_Qu__66603565");

            entity.HasOne(d => d.NguoiDuyetNavigation).WithMany(p => p.BaoCaoQsNgayNguoiDuyetNavigations)
                .HasForeignKey(d => d.NguoiDuyet)
                .HasConstraintName("FK__BAO_CAO_Q__Nguoi__656C112C");
        });

        modelBuilder.Entity<ChucVu>(entity =>
        {
            entity.HasKey(e => e.MaChucVu).HasName("PK__CHUC_VU__EFE3A5BA978FE7E4");

            entity.ToTable("CHUC_VU");

            entity.Property(e => e.MaChucVu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Chuc_vu");
            entity.Property(e => e.TenChucVu)
                .HasMaxLength(50)
                .HasColumnName("Ten_chuc_vu");
        });

        modelBuilder.Entity<DonVi>(entity =>
        {
            entity.HasKey(e => e.MaDonVi).HasName("PK__DON_VI__1254A856F325850B");

            entity.ToTable("DON_VI");

            entity.Property(e => e.MaDonVi)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Don_vi");
            entity.Property(e => e.TenDonVi)
                .HasMaxLength(20)
                .HasColumnName("Ten_don_vi");
        });

        modelBuilder.Entity<LoaiQuanNhan>(entity =>
        {
            entity.HasKey(e => e.MaLoai).HasName("PK__LOAI_QUA__41E2B1002D321C94");

            entity.ToTable("LOAI_QUAN_NHAN");

            entity.Property(e => e.MaLoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_loai");
            entity.Property(e => e.TenLoai)
                .HasMaxLength(20)
                .HasColumnName("Ten_loai");
        });

        modelBuilder.Entity<LsQsVang>(entity =>
        {
            entity.HasKey(e => e.MaLs).HasName("PK__LS_QS_VA__2E62BA62BFDC9C4A");

            entity.ToTable("LS_QS_VANG");

            entity.Property(e => e.MaLs)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_LS");
            entity.Property(e => e.CapBac)
                .HasMaxLength(20)
                .HasColumnName("Cap_bac");
            entity.Property(e => e.ChucVu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Chuc_vu");
            entity.Property(e => e.HoTen)
                .HasMaxLength(50)
                .HasColumnName("Ho_ten");
            entity.Property(e => e.LyDo)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ly_do");
            entity.Property(e => e.MaQuanNhan)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Quan_nhan");
            entity.Property(e => e.NgayDuyet)
                .HasColumnType("date")
                .HasColumnName("Ngay_duyet");
            entity.Property(e => e.NgayVang)
                .HasColumnType("date")
                .HasColumnName("Ngay_vang");
            entity.Property(e => e.NguoiDuyet)
                .HasMaxLength(50)
                .HasColumnName("Nguoi_duyet");
            entity.Property(e => e.TenDonVi)
                .HasMaxLength(20)
                .HasColumnName("Ten_don_vi");

            entity.HasOne(d => d.ChucVuNavigation).WithMany(p => p.LsQsVangs)
                .HasForeignKey(d => d.ChucVu)
                .HasConstraintName("FK__LS_QS_VAN__Chuc___6D0D32F4");

            entity.HasOne(d => d.LyDoNavigation).WithMany(p => p.LsQsVangs)
                .HasForeignKey(d => d.LyDo)
                .HasConstraintName("FK__LS_QS_VAN__Ly_do__6E01572D");

            entity.HasOne(d => d.MaQuanNhanNavigation).WithMany(p => p.LsQsVangs)
                .HasForeignKey(d => d.MaQuanNhan)
                .HasConstraintName("FK__LS_QS_VAN__Ma_Qu__6EF57B66");
        });

        modelBuilder.Entity<LyDo>(entity =>
        {
            entity.HasKey(e => e.MaLd).HasName("PK__LY_DO__2E62BA711CA3ECA7");

            entity.ToTable("LY_DO");

            entity.Property(e => e.MaLd)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_LD");
            entity.Property(e => e.LoaiLd)
                .HasMaxLength(50)
                .HasColumnName("Loai_LD");
        });

        modelBuilder.Entity<QuanNhan>(entity =>
        {
            entity.HasKey(e => e.MaQuanNhan).HasName("PK__QUAN_NHA__D03D19BE6CA8F11B");

            entity.ToTable("QUAN_NHAN");

            entity.Property(e => e.MaQuanNhan)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Quan_nhan");
            entity.Property(e => e.CapBac)
                .HasMaxLength(20)
                .HasColumnName("Cap_bac");
            entity.Property(e => e.ChucVu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Chuc_vu");
            entity.Property(e => e.DonVi)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Don_vi");
            entity.Property(e => e.HoTen)
                .HasMaxLength(50)
                .HasColumnName("Ho_ten");
            entity.Property(e => e.LoaiQn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Loai_QN");

            entity.HasOne(d => d.ChucVuNavigation).WithMany(p => p.QuanNhans)
                .HasForeignKey(d => d.ChucVu)
                .HasConstraintName("FK__QUAN_NHAN__Chuc___60A75C0F");

            entity.HasOne(d => d.DonViNavigation).WithMany(p => p.QuanNhans)
                .HasForeignKey(d => d.DonVi)
                .HasConstraintName("FK__QUAN_NHAN__Don_v__619B8048");

            entity.HasOne(d => d.LoaiQnNavigation).WithMany(p => p.QuanNhans)
                .HasForeignKey(d => d.LoaiQn)
                .HasConstraintName("fkLoaiQN");
        });

        modelBuilder.Entity<ThongBaoTrongNgay>(entity =>
        {
            entity.HasKey(e => e.MaThongBao).HasName("PK__THONG_BA__1C91EF9BCA2D2A99");

            entity.ToTable("THONG_BAO_TRONG_NGAY");

            entity.Property(e => e.MaThongBao)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Ma_Thong_bao");
            entity.Property(e => e.DonViNhan)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Don_vi_nhan");
            entity.Property(e => e.NgayGio)
                .HasColumnType("datetime")
                .HasColumnName("Ngay_gio");
            entity.Property(e => e.NguoiGui)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Nguoi_gui");
            entity.Property(e => e.NoiDung)
                .HasMaxLength(500)
                .HasColumnName("Noi_dung");

            entity.HasOne(d => d.DonViNhanNavigation).WithMany(p => p.ThongBaoTrongNgays)
                .HasForeignKey(d => d.DonViNhan)
                .HasConstraintName("FK__THONG_BAO__Don_v__6A30C649");

            entity.HasOne(d => d.NguoiGuiNavigation).WithMany(p => p.ThongBaoTrongNgays)
                .HasForeignKey(d => d.NguoiGui)
                .HasConstraintName("FK__THONG_BAO__Nguoi__693CA210");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
