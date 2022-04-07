using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EateryWebAPI.Models
{
    public class InfoRestaurant
    {
        public int MaNH { get; set; }
        public string TenNH { get; set; }
        public string DiaChi { get; set; }
        public string HinhAnh { get; set; }
        public string MoTa { get; set; }
        public int DanhGia { get; set; }
        public string TenTK { get; set; }
        public string MaLoaiNH { get; set; }
        public string TenLoaiNH { get; set; }
        public bool isDelete { get; set; }
    }
}