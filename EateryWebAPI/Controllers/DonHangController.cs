using EateryWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EateryWebAPI.Controllers
{
    public class DonHangController : ApiController
    {

        private EateryEntities db = new EateryEntities();

        [HttpGet]
        [Route("api/donhang")]
        public IHttpActionResult GetAllDonHang()
        {
            List<DONHANG> arr = db.DONHANGs.ToList();
            List<DONHANG> arrNH = new List<DONHANG>();
            foreach (var item in arr)
            {
                DONHANG dONHANG = new DONHANG();
                String name = db.NHAHANGs.SingleOrDefault(p => p.MaNH == item.MaNH).TenNH;
                TAIKHOAN taikhoan = db.TAIKHOANs.SingleOrDefault(p => p.TenTK == item.TenTK);
                dONHANG = item;
                dONHANG.TAIKHOAN = taikhoan;
                dONHANG.TAIKHOAN.DONHANGs = null;
                dONHANG.nameRes = name;
                dONHANG.NHAHANG = null;
                List<DONHANGCHITIET> listDHCT = db.DONHANGCHITIETs.Where(x => x.MaDHCT == item.MaDonHang).ToList();
                List<DONHANGCHITIET> listDHCTnew = new List<DONHANGCHITIET>();
                foreach (var item2 in listDHCT)
                {
                    DONHANGCHITIET dhct = new DONHANGCHITIET();
                    MONAN ma = db.MONANs.SingleOrDefault(p => p.MaMA == item2.MaMA);
                    dhct = item2;
                    dhct.TenMA = ma.TenMA;
                    dhct.giaMA = ma.Gia;
                    dhct.MONAN = null;
                    listDHCTnew.Add(dhct);

                }

                dONHANG.DONHANGCHITIETs = listDHCTnew;
                dONHANG.countSL = listDHCT.Count();
                dONHANG.TongTien = listDHCTnew.Sum(x => x.DonGia);
                arrNH.Add(dONHANG);
            }
            return Ok(arr);
        }

        [HttpGet]
        [Route("api/donhang/{id}")]
        public IHttpActionResult GetDonHangByID(int id)
        {
            DONHANG _model = db.DONHANGs.Where(x => x.MaDonHang == id).FirstOrDefault();
            return Ok(_model);
        }

        //DonHangFM - FINISHED
        [HttpGet]
        [Route("api/getAllDonHangTheoTK")]
        public IHttpActionResult GetAllDonhangTheoTenTK(String TenTK)
        {

            List<DONHANG> arr = db.DONHANGs.Where(x => x.TenTK == TenTK && x.TrangThaiDH != 0).ToList();
            List<DONHANG> arrNH = new List<DONHANG>();
            foreach (var item in arr)
            {
                DONHANG dONHANG = new DONHANG();
                String name = db.NHAHANGs.SingleOrDefault(p => p.MaNH == item.MaNH).TenNH;
                dONHANG = item;
                dONHANG.nameRes = name;
                dONHANG.NHAHANG = null;
                dONHANG.TAIKHOAN = db.TAIKHOANs.SingleOrDefault(x => x.TenTK == TenTK);  
                List<DONHANGCHITIET> listDHCT = db.DONHANGCHITIETs.Where(x => x.MaDHCT == item.MaDonHang).ToList();
                List<DONHANGCHITIET> listDHCTnew = new List<DONHANGCHITIET>();
                foreach (var item2 in listDHCT)
                {
                    DONHANGCHITIET dhct = new DONHANGCHITIET();
                    MONAN ma = db.MONANs.SingleOrDefault(p => p.MaMA == item2.MaMA);
                    dhct = item2;
                    dhct.TenMA = ma.TenMA;
                    dhct.giaMA = ma.Gia;
                    dhct.MaMA = ma.MaMA;
                    dhct.MONAN = null;
                    listDHCTnew.Add(dhct);

                }
                
                dONHANG.DONHANGCHITIETs = listDHCTnew;
                dONHANG.countSL = listDHCT.Count();
                dONHANG.TongTien = listDHCTnew.Sum(x => x.DonGia);
                arrNH.Add(dONHANG);
            }

            return Ok(arrNH);
        }

        //ThanhToanFM
        [HttpGet]
        [Route("api/getDonHangTheoTK")]
        public IHttpActionResult GetDonhangTheoTenTK(String TenTK)
        {

            DONHANG donhang = db.DONHANGs.SingleOrDefault(x => x.TenTK == TenTK && x.TrangThaiDH == 0);
            if(donhang == null)
            {
                return Ok(new Message(0, "Lấy đơn hàng không tồn tại hoặc đang ở một trạng thái khác"));
            }
            String name = db.NHAHANGs.SingleOrDefault(p => p.MaNH == donhang.MaNH).TenNH;
            donhang.nameRes = name;
            donhang.NHAHANG = null;

            List<DONHANGCHITIET> listDHCT = db.DONHANGCHITIETs.Where(x => x.MaDHCT == donhang.MaDonHang).ToList();
            List<DONHANGCHITIET> listDHCTnew = new List<DONHANGCHITIET>();
            foreach (var item2 in listDHCT)
            {
                DONHANGCHITIET dhct = new DONHANGCHITIET();
                MONAN ma = db.MONANs.SingleOrDefault(p => p.MaMA == item2.MaMA);
                dhct = item2;
                dhct.HinhAnhMA = ma.HinhAnh;
                dhct.TenMA = ma.TenMA;
                dhct.giaMA = ma.Gia;
                dhct.DonGia = (double)(dhct.SL*ma.Gia);
                dhct.MONAN = null;
                listDHCTnew.Add(dhct);

            }

            donhang.DONHANGCHITIETs = listDHCTnew;
            donhang.countSL = listDHCT.Count();
            donhang.TongTien = db.DONHANGCHITIETs.Where(x => x.MaDHCT == donhang.MaDonHang).Sum(x => x.DonGia);
            return Ok(donhang);
        }

        [HttpPost]
        [Route("api/themdonhang")]
        public IHttpActionResult GetAllDonhangTheoTenTK(DONHANG donhang)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new Message(0, "Thêm đơn hàng thất bại"));
            }
            db.DONHANGs.AddOrUpdate(donhang);
            db.SaveChanges();
            return Ok(new Message(1, "Thêm đơn hàng thành công"));
        }

        [HttpGet]
        [Route("api/xoaDonHangTheoTK")]
        public IHttpActionResult XoaDonHangCuaTenTKTheoMaDH(int MaDH, String TenTK)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new Message(0, "Xoá đơn hàng Failed"));
            }
            DONHANG _model = db.DONHANGs.SingleOrDefault(x => x.MaDonHang == MaDH && x.TenTK == TenTK);
            if (_model == null)
            {
                return Ok(new Message(0, "Xoá đơn hàng Failed"));
            }
            db.DONHANGs.Remove(_model);
            db.SaveChanges();
            return Ok(new Message(1, "Xoá đơn hàng thành công"));
        }

        //ThanhToanFM
        [HttpPost]
        [Route("api/CapNhatTrangThaiDonHangCuaTK")]
        public IHttpActionResult CapNhatTrangThaiDonHang(DonHangModel donhang)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new Message(0, "Error"));
            }

            DONHANG _donhang = db.DONHANGs.SingleOrDefault(x => x.TenTK == x.TenTK && x.MaDonHang == donhang.MaDonHang);
            if(_donhang == null)
            {
                return Ok(new Message(0, "Đơn hàng không tồn tại"));
            }
            _donhang.DiaChi = donhang.DiaChi;
            _donhang.TrangThaiDH = donhang.TrangThaiDH;
            _donhang.TongTien = donhang.TongTien;
            _donhang.NgayMua = DateTime.Now;

            db.DONHANGs.AddOrUpdate(_donhang);
            db.SaveChanges();
            return Ok(new Message(1, "Đơn hàng đã được gửi và đang chờ xác nhận"));
        }

        //CCH_ItemQuanLyDonHangFM - đang chờ - đã xong
        [HttpGet]
        [Route("api/getAllDonHangTheoMaNHTheoTTDH")]
        public IHttpActionResult GetAllDonHangTheoMaNHDuaVaoTrangThai(int MaNH, int TrangThaiDH)
        {

            List<DONHANG> arr = db.DONHANGs.Where(x => x.MaNH == MaNH && x.TrangThaiDH == TrangThaiDH).ToList();
            
            return Ok(arr);
        }

        //CCH_ChiTietDonHangFM
        [HttpPost]
        [Route("api/capNhatTrangThaiDHCuaChuCuaHang")]
        public IHttpActionResult CapNhatTrangThaiDHCuaChuCuaHang(int MaDH, int TrangThaiDH)
        {
            DONHANG _donhang = db.DONHANGs.SingleOrDefault(x => x.MaDonHang == MaDH && x.TrangThaiDH != 0);
            if (_donhang == null)
            {
                return Ok(new Message(0, "Đơn hàng không tồn tại"));
            }
            _donhang.TrangThaiDH = TrangThaiDH;

            db.DONHANGs.AddOrUpdate(_donhang);
            db.SaveChanges();
            return Ok(new Message(1, "Đơn hàng đã xác nhận"));
        }

        //ThanhToanFM - cập nhật địa chỉ giao hàng
        [HttpPost]
        [Route("api/CapNhatDiaChiGiaoHang")]
        public IHttpActionResult CapNhatDiaChiGiaoHang(int MaDH, string DiaChiMoi)
        {
            DONHANG _donhang = db.DONHANGs.SingleOrDefault(x => x.MaDonHang == MaDH && x.TrangThaiDH == 0);
            if (_donhang == null)
            {
                return Ok(new Message(0, "Đơn hàng không tồn tại"));
            }
            _donhang.DiaChi = DiaChiMoi;

            db.DONHANGs.AddOrUpdate(_donhang);
            db.SaveChanges();
            string diaChi = db.DONHANGs.SingleOrDefault(x => x.MaDonHang == MaDH && x.TrangThaiDH == 0).DiaChi;
            return Ok(diaChi);
        }

        //CCH_ChiTietDonHangFM
        [HttpGet]
        [Route("api/LichSuNhungDonHangTrong1NgayCuaNH")]
        public IHttpActionResult LichSuNhungDonHangTrong1NgayCuaNH(int MaNH, DateTime NgayMua)
        {
            List<DONHANG> arr = db.DONHANGs.Where(x => x.MaNH == MaNH && x.NgayMua == NgayMua && x.TrangThaiDH != 0).ToList();
            
            return Ok(arr);
        }

        //CCH_ChiTietDonHangFM
        [HttpGet]
        [Route("api/GetTongTienCuaDonHang")]
        public IHttpActionResult GetTongTienCuaDonHang(int MaDH)
        {
            DONHANG dh = db.DONHANGs.SingleOrDefault(x=>x.MaDonHang == MaDH);
            List<DONHANGCHITIET> listDHCT = db.DONHANGCHITIETs.Where(x => x.MaDHCT == dh.MaDonHang).ToList();
            double TongTien = listDHCT.Sum(x => x.DonGia);
            return Ok(TongTien);
        }

        //ThongKe
        [HttpGet]
        [Route("api/GetTongDoanhThuDonHangTheoNH")]
        public IHttpActionResult GetTongDoanhThuDonHangTheoNH(int MaNH, DateTime TuNgay, DateTime DenNgay)
        {
            List<DONHANG> arrDH = db.DONHANGs.Where(x=>x.MaNH == MaNH && x.TrangThaiDH != 0 && (x.NgayMua>=TuNgay && x.NgayMua<=DenNgay)).ToList();
            double tt = 0;
            foreach(var item in arrDH)
            {
                double tongTien = item.TongTien;
                tt += tongTien;
            }
            return Ok(tt);
        }
    }
}
