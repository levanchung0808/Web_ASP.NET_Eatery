namespace EateryWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eatery.NHAHANG")]
    public partial class NHAHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHAHANG()
        {
            DONHANGs = new HashSet<DONHANG>();
            KHUYENMAIs = new HashSet<KHUYENMAI>();
            MONANs = new HashSet<MONAN>();
        }

        [Key]
        public int MaNH { get; set; }

        [Required]
        [StringLength(255)]
        public string TenNH { get; set; }

        [Required]
        [StringLength(255)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(255)]
        public string HinhAnh { get; set; }

        [Required]
        [StringLength(255)]
        public string MoTa { get; set; }

        public int DanhGia { get; set; }

        [Required]
        [StringLength(255)]
        public string TenTK { get; set; }

        public string HoTen;

        [StringLength(255)]
        public string MaLoaiNH { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KHUYENMAI> KHUYENMAIs { get; set; }

        public virtual LoaiNH LoaiNH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MONAN> MONANs { get; set; }

        public virtual TAIKHOAN TAIKHOAN { get; set; }
    }
}
