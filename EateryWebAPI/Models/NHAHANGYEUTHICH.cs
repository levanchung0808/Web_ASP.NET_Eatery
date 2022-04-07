namespace EateryWebAPI.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("eatery.NHAHANGYEUTHICH")]
    public partial class NHAHANGYEUTHICH
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(255)]
        public string TenTK { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MaNH { get; set; }
    }
}
