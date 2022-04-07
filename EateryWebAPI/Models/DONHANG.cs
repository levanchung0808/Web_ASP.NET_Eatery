namespace EateryWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eatery.DONHANG")]
    public partial class DONHANG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DONHANG()
        {
            DONHANGCHITIETs = new HashSet<DONHANGCHITIET>();
        }

        [Key]
        public int MaDonHang { get; set; }

        [Required]
        [StringLength(255)]
        public string DiaChi { get; set; }

        public int TrangThaiDH { get; set; }

        public double TongTien { get; set; }

        [Column(TypeName = "date")]
        public DateTime NgayMua { get; set; }

        [Required]
        [StringLength(255)]
        public string TenTK { get; set; }

        public int MaNH { get; set; }

        public virtual NHAHANG NHAHANG { get; set; }

        public virtual TAIKHOAN TAIKHOAN { get; set; }

        public String nameRes;

        public int countSL;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANGCHITIET> DONHANGCHITIETs { get; set; }
    }
}
