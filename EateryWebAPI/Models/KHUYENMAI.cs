namespace EateryWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eatery.KHUYENMAI")]
    public partial class KHUYENMAI
    {
        [Key]
        [StringLength(255)]
        public string MaKM { get; set; }

        [Required]
        [StringLength(255)]
        public string TenKhuyenMai { get; set; }

        public int TienKM { get; set; }

        public int SL { get; set; }

        public int MaNH { get; set; }

        [Required]
        [StringLength(255)]
        public string HinhAnh { get; set; }

        public virtual NHAHANG NHAHANG { get; set; }
    }
}
