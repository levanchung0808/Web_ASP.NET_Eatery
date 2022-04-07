namespace EateryWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eatery.DONHANGCHITIET")]
    public partial class DONHANGCHITIET
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaDHCT { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaMA { get; set; }

        public int SL { get; set; }

        public String TenMA;
        public double? giaMA;

        public double DonGia { get; set; }
        public string HinhAnhMA;

        public virtual DONHANG DONHANG { get; set; }

        public virtual MONAN MONAN { get; set; }
    }
}
