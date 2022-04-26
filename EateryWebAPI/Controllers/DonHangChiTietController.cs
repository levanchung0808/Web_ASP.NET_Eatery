using EateryWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
                _donhang.TongTien = 0;
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
            MONAN ma = db.MONANs.SingleOrDefault(x => x.MaMA == MaMA);
            if (_dhct != null)
            {
                //sl = sl hien tai + sl getText
                _dhct.SL = _dhct.SL + SL;
                _dhct.DonGia = (double)(ma.Gia * _dhct.SL);
                db.DONHANGCHITIETs.AddOrUpdate(_dhct);
                db.SaveChanges();

                dh.TongTien = db.DONHANGCHITIETs.Where(x => x.MaDHCT == dh.MaDonHang).Sum(x => x.DonGia);
                db.DONHANGs.AddOrUpdate(dh);
                db.SaveChanges();
                return Ok(new Message(3, "Số lượng món tăng đã +" + SL + " trong giỏ hàng"));
            }

            DONHANGCHITIET dhct = new DONHANGCHITIET();
            dhct.MaDHCT = dh.MaDonHang;
            dhct.MaMA = MaMA;
            dhct.SL = SL;
            dhct.DonGia = (double)ma.Gia * SL;

            db.DONHANGCHITIETs.Add(dhct);
            db.SaveChanges();

            dh.TongTien = db.DONHANGCHITIETs.Where(x => x.MaDHCT == dh.MaDonHang).Sum(x => x.DonGia);
            db.DONHANGs.AddOrUpdate(dh);
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
            DONHANG donhang = db.DONHANGs.SingleOrDefault(x => x.MaDonHang == MaDHCT && x.TrangThaiDH == 0);

            DONHANGCHITIET dhct = db.DONHANGCHITIETs.SingleOrDefault(x => x.MaDHCT == MaDHCT && x.MaMA == MaMA);
            db.DONHANGCHITIETs.Remove(dhct);
            db.SaveChanges();
            
            List<DONHANGCHITIET> arrDHCT = db.DONHANGCHITIETs.Where(x => x.MaDHCT == donhang.MaDonHang).ToList();
            //List<DONHANGCHITIET> arrDHCTnew = new List<DONHANGCHITIET>();
            //foreach (var item in arrDHCT)
            //{
            //    MONAN monan = db.MONANs.SingleOrDefault(x => x.MaMA == item.MaMA);
            //    item.TenMA = monan.TenMA;
            //    item.giaMA = monan.Gia;
            //    item.HinhAnhMA = monan.HinhAnh;
            //    item.MONAN = null;
            //    item.DONHANG = null;
            //    item.DonGia = (double)(monan.Gia * item.SL);
            //    arrDHCTnew.Add(item);
            //}
            //donhang.MaDonHang = donhang.MaDonHang;
            //donhang.DONHANGCHITIETs = arrDHCTnew;

            
            if (donhang.DONHANGCHITIETs.Count() == 0)
            {
                donhang.TongTien = 0;
            }
            else
            {
                donhang.TongTien = arrDHCT.Sum(x => x.DonGia);
            }

            db.DONHANGs.AddOrUpdate(donhang);
            db.SaveChanges();

            foreach (var item in arrDHCT)
            {
                MONAN monan = db.MONANs.SingleOrDefault(x => x.MaMA == item.MaMA);
                item.TenMA = monan.TenMA;
                item.giaMA = monan.Gia;
                item.HinhAnhMA = monan.HinhAnh;
                item.MONAN = null;
                item.DONHANG = null;
                item.MaMA = item.MaMA;
                item.DonGia = (double)(monan.Gia * item.SL);
            }

            return Ok(arrDHCT);
        }


        //ThanhToanFM - cập nhật số lượng + tổng tiền sau khi tăng, giảm sl món ăn trong recyclerview
        [HttpPost]
        [Route("api/CapNhatSoLuongTangGiamMonAn")]
        public IHttpActionResult CapNhatSoLuongTangGiamMonAn(int MaDHCT, int MaMA, string tanghoacgiam)
        {
            DONHANG donhang = db.DONHANGs.SingleOrDefault(x => x.MaDonHang == MaDHCT && x.TrangThaiDH == 0);
            DONHANGCHITIET _dhct = db.DONHANGCHITIETs.SingleOrDefault(x => x.MaDHCT == donhang.MaDonHang && x.MaMA == MaMA);
            double GiaMA = (double)db.MONANs.SingleOrDefault(x => x.MaMA == MaMA).Gia;
            if (_dhct != null)
            {
                //nếu tanghoacgiam = "tang"
                if(tanghoacgiam == "tang")
                {
                    _dhct.SL = _dhct.SL + 1;
                    _dhct.DonGia = GiaMA * _dhct.SL;
                    db.DONHANGCHITIETs.AddOrUpdate(_dhct);
                    db.SaveChanges();
                }else if(tanghoacgiam == "giam")
                {
                    ////số lượng hiện tại = 1 mà ấn giảm nữa thì SL = 0 => xoá đơn hàng chi tiết
                    if(_dhct.SL == 1)
                    {
                        _dhct.SL = _dhct.SL - 1;
                        _dhct.DonGia = GiaMA * _dhct.SL;
                        db.DONHANGCHITIETs.AddOrUpdate(_dhct);
                        db.SaveChanges();

                        double TT = 1314;
                        return Ok(TT);
                        //gọi api xoá món ăn có sl = 0
                    }
                    _dhct.SL = _dhct.SL - 1;
                    _dhct.DonGia = GiaMA * _dhct.SL;
                    db.DONHANGCHITIETs.AddOrUpdate(_dhct);
                    db.SaveChanges();
                }
            }

            double TongTien = db.DONHANGCHITIETs.Where(x=>x.MaDHCT == MaDHCT).Sum(x=>x.DonGia);
            donhang.TongTien = TongTien;
            db.DONHANGs.AddOrUpdate(donhang);
            db.SaveChanges();
            return Ok((double)donhang.TongTien);
        }
    }
}
