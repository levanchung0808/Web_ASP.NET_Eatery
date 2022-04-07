namespace EateryWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eatery.TAIKHOAN")]
    public partial class TAIKHOAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TAIKHOAN()
        {
            DONHANGs = new HashSet<DONHANG>();
            NHAHANGs = new HashSet<NHAHANG>();
        }

        [Key]
        [StringLength(255)]
        public string TenTK { get; set; }

        [Required]
        [StringLength(255)]
        public string MatKhau { get; set; }

        [Required]
        [StringLength(255)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(255)]
        public string SDT { get; set; }

        [Required]
        [StringLength(255)]
        public string DiaChi { get; set; }

        [Required]
        [StringLength(255)]
        public string HinhAnh { get; set; }

        [Required]
        [StringLength(255)]
        public string VaiTro { get; set; }

        public string CodeForgotPassword { get; set; }

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANG> DONHANGs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAHANG> NHAHANGs { get; set; }
    }
}
