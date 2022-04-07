using EateryWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EateryWebAPI.Controllers
{
    public class DonHangChiTietController : ApiController
    {
        private EateryEntities db = new EateryEntities();

        [HttpGet]
        [Route("api/donhangchitiet")]
        public IHttpActionResult GetAllDonHangChiTiet()
        {
            List<DONHANGCHITIET> arr = db.DONHANGCHITIETs.ToList();
            DONHANGCHITIET dhct = new DONHANGCHITIET();
            foreach(var item in arr)
            {
                MONAN monan = db.MONANs.SingleOrDefault(x => x.MaMA == item.MaMA);
                dhct.MaDHCT = item.MaDHCT;
                dhct.MaMA = monan.MaMA;
                dhct.TenMA = monan.TenMA;
                dhct.giaMA = monan.Gia;
                dhct.DonGia = item.DonGia;
                dhct.SL = item.SL;
            }
            return Ok(arr);
        }

        //ChiTietMonAnFM - btn Thêm vào giỏ hàng - FINISHHED
        [HttpPost]
        [Route("api/ThemMonAnVaoGioHang")]
        public IHttpActionResult ThemMonAnVaoGioHang(string TenTK, int MaMA, int SL)
        {
            DONHANG donhang = db.DONHANGs.SingleOrDefault(x => x.TenTK == TenTK && x.TrangThaiDH == 0);
            if(donhang == null)
            {
                //chưa có đơn hàng thì tạo mới 1 đơn hàng để lưu
                DONHANG _donhang = new DONHANG();
                _donhang.DiaChi = db.TAIKHOANs.SingleOrDefault(x=>x.TenTK == TenTK).DiaChi;
                _donhang.TrangThaiDH = 0;
                _donhang.NgayMua = DateTime.Now;
                _donhang.TenTK = TenTK;
                _donhang.TongTien = (double)db.MONANs.SingleOrDefault(x => x.MaMA == MaMA).Gia*SL;
                _donhang.MaNH = db.MONANs.SingleOrDefault(x=>x.MaMA == MaMA).MaNH;
                db.DONHANGs.Add(_donhang);
                db.SaveChanges();
            }

            //Kiểm tra món ăn trong đơn hàng có cùng nhà hàng hay không
            DONHANG dh = db.DONHANGs.SingleOrDefault(x => x.TenTK == TenTK && x.TrangThaiDH == 0);
            int MaNH = db.MONANs.SingleOrDefault(x => x.MaMA == MaMA).MaNH;
            if (dh.MaNH != MaNH)
            {
                return Ok(new Message(2, "Món ăn khác nhà hàng"));
            }

            DONHANGCHITIET _dhct = db.DONHANGCHITIETs.SingleOrDefault(x => x.MaDHCT == dh.MaDonHang &&  x.MaMA == MaMA); 
            if(_dhct != null)
            {
                return Ok(new Message(0, "Món ăn đã tồn tại trong giỏ hàng"));
            }

            DONHANGCHITIET dhct = new DONHANGCHITIET();
            dhct.MaDHCT = dh.MaDonHang;
            dhct.MaMA = MaMA;
            dhct.SL = SL;
            dhct.DonGia = (double)db.MONANs.SingleOrDefault(x => x.MaMA == MaMA).Gia * SL;

            db.DONHANGCHITIETs.Add(dhct);
            db.SaveChanges();
            return Ok(new Message(1,"Thêm món ăn vào giỏ hàng thành công"));
        }

        //Kiểm tra món ăn trong đơn hàng có cùng nhà hàng hay không
        [HttpPost]
        [Route("api/XoaDonHangKhiDatMonAnKhacNhaHang")]
        public IHttpActionResult XoaDonHangKhiDatMonAnKhacNhaHang(string TenTK)
        {
            DONHANG dh = db.DONHANGs.SingleOrDefault(x => x.TenTK == TenTK && x.TrangThaiDH == 0);
            List<DONHANGCHITIET> _dhctRemove = db.DONHANGCHITIETs.ToList();
            _dhctRemove.RemoveAll(x => x.MaDHCT == dh.MaDonHang);
            db.DONHANGs.Remove(dh);
            db.SaveChanges();
            return Ok(new Message(1, "Xoá DHCT và Đơn hàng thành công"));
        }

        //DonHangChiTietFM
        [HttpGet]
        [Route("api/donhangchitiet")]
        public IHttpActionResult GetDonHangChiTiet(int MaDHCT)
        {
            DONHANGCHITIET _donhangchitiet = db.DONHANGCHITIETs.SingleOrDefault(x => x.MaDHCT == MaDHCT);
            return Ok(_donhangchitiet);
        }

        //DonHangChiTietFM
        [HttpPost]
        [Route("api/taodonhang")]
        public IHttpActionResult TaoDonHang(DONHANG donhang)
        {
            DONHANG _donhang = new DONHANG();
            _donhang.DiaChi = db.TAIKHOANs.SingleOrDefault(x=>x.TenTK == donhang.TenTK).DiaChi;
            _donhang.TrangThaiDH = 0;
            _donhang.NgayMua = DateTime.Now;
            _donhang.TenTK = donhang.TenTK;
            _donhang.MaNH = donhang.MaNH;
            db.DONHANGs.Add(donhang);
            db.SaveChanges();
            return Ok(new Message(1, "Tạo đơn hàng thành công"));
        }

        //ThanhToan - xoá món ăn trong đơn hàng
        [HttpPost]
        [Route("api/XoaMonAnTrongDonHang")]
        public IHttpActionResult XoaMonAnTrongDonHang(int MaDHCT, int MaMA)
        {
            DONHANGCHITIET dhct = db.DONHANGCHITIETs.SingleOrDefault(x => x.MaDHCT == MaDHCT && x.MaMA == MaMA);
            if(dhct == null)
            {
                return Ok(new Message(0, "Món ăn không tồn tại trong đơn hàng để xoá"));
            }
            db.DONHANGCHITIETs.Remove(dhct);
            db.SaveChanges();
            List<DONHANGCHITIET> arrDH = db.DONHANGCHITIETs.Where(x => x.MaDHCT == MaDHCT).ToList();
            foreach(var item in arrDH)
            {
                MONAN monan = db.MONANs.SingleOrDefault(x => x.MaMA == item.MaMA);
                item.TenMA = monan.TenMA;
                item.giaMA = monan.Gia;
                item.HinhAnhMA = monan.HinhAnh;
                item.MONAN = null;
            }
            //return Ok(new Message(1, "Xoá món ăn trong đơn hàng thành công"));
            return Ok(arrDH);
        }
    }
}
