using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EateryWebAPI.Models
{
    public class DangNhapModel
    {
        public string TenTK { get; set; }
        public string MatKhau { get; set; }

        public DangNhapModel(string tenTK, string matKhau)
        {
            TenTK = tenTK;
            MatKhau = matKhau;
        }
    }
}