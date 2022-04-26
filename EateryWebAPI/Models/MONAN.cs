namespace EateryWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eatery.MONAN")]
    public partial class MONAN
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MONAN()
        {
            DONHANGCHITIETs = new HashSet<DONHANGCHITIET>();
        }

        [Key]
        public int MaMA { get; set; }

        [StringLength(255)]
        public string TenMA { get; set; }

        public double? Gia { get; set; }

        [StringLength(255)]
        public string HinhAnh { get; set; }

        public int MaNH { get; set; }

        public string TenNH;

        public bool isDelete { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONHANGCHITIET> DONHANGCHITIETs { get; set; }

        public virtual NHAHANG NHAHANG { get; set; }
    }
}
