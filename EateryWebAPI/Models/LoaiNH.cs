namespace EateryWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eatery.LoaiNH")]
    public partial class LoaiNH
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiNH()
        {
            NHAHANGs = new HashSet<NHAHANG>();
        }

        [Key]
        [StringLength(255)]
        public string MaLoaiNH { get; set; }

        [StringLength(255)]
        public string TenLoaiNH { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHAHANG> NHAHANGs { get; set; }
    }
}
