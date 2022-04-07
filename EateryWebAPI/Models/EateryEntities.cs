using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EateryWebAPI.Models
{
    public partial class EateryEntities : DbContext
    {
        public EateryEntities()
            : base("name=EateryEntities")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<DONHANG> DONHANGs { get; set; }
        public virtual DbSet<DONHANGCHITIET> DONHANGCHITIETs { get; set; }
        public virtual DbSet<KHUYENMAI> KHUYENMAIs { get; set; }
        public virtual DbSet<LoaiNH> LoaiNHs { get; set; }
        public virtual DbSet<MONAN> MONANs { get; set; }
        public virtual DbSet<NHAHANG> NHAHANGs { get; set; }
        public virtual DbSet<NHAHANGYEUTHICH> NHAHANGYEUTHICHes { get; set; }
        public virtual DbSet<TAIKHOAN> TAIKHOANs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DONHANG>()
                .Property(e => e.TenTK)
                .IsUnicode(false);

            modelBuilder.Entity<DONHANG>()
                .HasMany(e => e.DONHANGCHITIETs)
                .WithRequired(e => e.DONHANG)
                .HasForeignKey(e => e.MaDHCT)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<KHUYENMAI>()
                .Property(e => e.MaKM)
                .IsUnicode(false);

            modelBuilder.Entity<KHUYENMAI>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<LoaiNH>()
                .Property(e => e.MaLoaiNH)
                .IsUnicode(false);

            modelBuilder.Entity<MONAN>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<MONAN>()
                .HasMany(e => e.DONHANGCHITIETs)
                .WithRequired(e => e.MONAN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHAHANG>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<NHAHANG>()
                .Property(e => e.TenTK)
                .IsUnicode(false);

            modelBuilder.Entity<NHAHANG>()
                .Property(e => e.MaLoaiNH)
                .IsUnicode(false);

            modelBuilder.Entity<NHAHANG>()
                .HasMany(e => e.DONHANGs)
                .WithRequired(e => e.NHAHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHAHANG>()
                .HasMany(e => e.KHUYENMAIs)
                .WithRequired(e => e.NHAHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHAHANG>()
                .HasMany(e => e.MONANs)
                .WithRequired(e => e.NHAHANG)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NHAHANGYEUTHICH>()
                .Property(e => e.TenTK)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.TenTK)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.MatKhau)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.SDT)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.HinhAnh)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .Property(e => e.VaiTro)
                .IsUnicode(false);

            modelBuilder.Entity<TAIKHOAN>()
                .HasMany(e => e.DONHANGs)
                .WithRequired(e => e.TAIKHOAN)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TAIKHOAN>()
                .HasMany(e => e.NHAHANGs)
                .WithRequired(e => e.TAIKHOAN)
                .WillCascadeOnDelete(false);
        }
    }
}
