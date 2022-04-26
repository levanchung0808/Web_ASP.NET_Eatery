using EateryWebAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Windows.Documents;

namespace EateryWebAPI.Controllers
{
    public class NhaHangController : ApiController
    {
        private EateryEntities db = new EateryEntities();

        [HttpGet]
        [Route("api/nhahang")]
        public IHttpActionResult GetAllNhaHang()
        {
            List<NHAHANG> arr = db.NHAHANGs.OrderByDescending(x => x.DanhGia).ToList();
            List<InfoRestaurant> arrRes = new List<InfoRestaurant>();
            foreach (var item in arr)
            {
                String tenNH = db.LoaiNHs.SingleOrDefault(p => p.MaLoaiNH == item.MaLoaiNH).TenLoaiNH;
                InfoRestaurant infoRestaurant = new InfoRestaurant();
                infoRestaurant.MaNH = item.MaNH;
                infoRestaurant.TenNH = item.TenNH;
                infoRestaurant.DiaChi = item.DiaChi;
                infoRestaurant.HinhAnh = item.HinhAnh;
                infoRestaurant.MoTa = item.MoTa;
                infoRestaurant.DanhGia = item.DanhGia;
                infoRestaurant.TenTK = item.TenTK;
                infoRestaurant.MaLoaiNH = item.MaLoaiNH;
                infoRestaurant.TenLoaiNH = tenNH;
                infoRestaurant.isDelete = item.isDelete;
                arrRes.Add(infoRestaurant);
            }

            return Ok(arrRes);
        }

        [HttpGet]
        [Route("api/GetAllNhaHangChuaXoa")]
        public IHttpActionResult GetAllNhaHangChuaXoa()
        {
            List<NHAHANG> arr = db.NHAHANGs.Where(x=>x.isDelete == false).OrderByDescending(x => x.DanhGia).ToList();
            List<InfoRestaurant> arrRes = new List<InfoRestaurant>();
            foreach (var item in arr)
            {
                String tenNH = db.LoaiNHs.SingleOrDefault(p => p.MaLoaiNH == item.MaLoaiNH).TenLoaiNH;
                InfoRestaurant infoRestaurant = new InfoRestaurant();
                infoRestaurant.MaNH = item.MaNH;
                infoRestaurant.TenNH = item.TenNH;
                infoRestaurant.DiaChi = item.DiaChi;
                infoRestaurant.HinhAnh = item.HinhAnh;
                infoRestaurant.MoTa = item.MoTa;
                infoRestaurant.DanhGia = item.DanhGia;
                infoRestaurant.TenTK = item.TenTK;
                infoRestaurant.MaLoaiNH = item.MaLoaiNH;
                infoRestaurant.TenLoaiNH = tenNH;
                infoRestaurant.isDelete = item.isDelete;
                arrRes.Add(infoRestaurant);
            }

            return Ok(arrRes);
        }

        [HttpGet]
        [Route("api/nhahangyeuthich")]
        public IHttpActionResult GetNhaHangYeuThich(int MaNH, String TenTK)
        {
            NHAHANG _model = db.NHAHANGs.Where(x => x.MaNH == MaNH).FirstOrDefault();
            String tenNH = db.LoaiNHs.Where(p => p.MaLoaiNH == _model.MaLoaiNH).FirstOrDefault().TenLoaiNH;
            NHAHANGYEUTHICH _nhahangyeuthich = db.NHAHANGYEUTHICHes.SingleOrDefault(x => x.TenTK == TenTK && x.MaNH == MaNH);

            InfoRestaurant infoRestaurant = new InfoRestaurant();
            infoRestaurant.MaNH = _model.MaNH;
            infoRestaurant.TenNH = _model.TenNH;
            infoRestaurant.DiaChi = _model.DiaChi;
            infoRestaurant.HinhAnh = _model.HinhAnh;
            infoRestaurant.MoTa = _model.MoTa;
            infoRestaurant.DanhGia = _model.DanhGia;
            infoRestaurant.TenTK = _model.TenTK;
            infoRestaurant.MaLoaiNH = _model.MaLoaiNH;
            infoRestaurant.TenLoaiNH = tenNH;
            return Ok(infoRestaurant);
        }

        [HttpGet]
        [Route("api/nhahangtheoloai/{loaiNH}")]
        public IHttpActionResult GetAllNhaHangTheoLoai(string loaiNH)
        {
            List<NHAHANG> arr = db.NHAHANGs.Where(p => p.MaLoaiNH == loaiNH).ToList();
            List<InfoRestaurant> arrNHTheoLoai = new List<InfoRestaurant>();
            foreach (NHAHANG item in arr)
            {

                String tenNH = db.LoaiNHs.Where(p => p.MaLoaiNH == item.MaLoaiNH).FirstOrDefault().TenLoaiNH;
                InfoRestaurant infoRestaurant = new InfoRestaurant();
                infoRestaurant.MaNH = item.MaNH;
                infoRestaurant.TenNH = item.TenNH;
                infoRestaurant.DiaChi = item.DiaChi;
                infoRestaurant.HinhAnh = item.HinhAnh;
                infoRestaurant.MoTa = item.MoTa;
                infoRestaurant.DanhGia = item.DanhGia;
                infoRestaurant.TenTK = item.TenTK;
                infoRestaurant.MaLoaiNH = item.MaLoaiNH;
                infoRestaurant.TenLoaiNH = tenNH;
                arrNHTheoLoai.Add(infoRestaurant);
                item.LoaiNH = null;
            }


            return Ok(arrNHTheoLoai);
        }

        [HttpGet]
        [Route("api/nhahangtheomanh/{MaNH}")]
        public IHttpActionResult GetNhaHangTheoMaNH(int MaNH)
        {
            NHAHANG nhahang = db.NHAHANGs.SingleOrDefault(p => p.MaNH == MaNH);
            String hoTen = db.TAIKHOANs.SingleOrDefault(x => x.TenTK == nhahang.TenTK).HoTen;
            nhahang.HoTen = hoTen;
            nhahang.TAIKHOAN = null;
            return Ok(nhahang);
        }

        [HttpGet]
        [Route("api/nhahangtheotentk")]
        public IHttpActionResult GetNhaHangTheoTenTK(string TenTK)
        {
            String hoTen = db.TAIKHOANs.SingleOrDefault(x => x.TenTK == TenTK).HoTen;
            NHAHANG nhahang = db.NHAHANGs.SingleOrDefault(p => p.TenTK == TenTK);
            nhahang.HoTen = hoTen;

            return Ok(nhahang);
        }

        [HttpPost]
        [Route("api/ChinhSuaThongTinNhaHang")]
        public IHttpActionResult ChinhSuaThongTinNhaHang(NHAHANG nhahang)
        {
            NHAHANG _nhahang = db.NHAHANGs.SingleOrDefault(x => x.MaNH == nhahang.MaNH);
            if (_nhahang == null)
            {
                return Ok(new Message(0, "Nhà hàng không tồn tại"));
            }
            _nhahang.TenNH = nhahang.TenNH;
            _nhahang.DiaChi = nhahang.DiaChi;
            _nhahang.MoTa = nhahang.MoTa;
            db.NHAHANGs.AddOrUpdate(_nhahang);
            db.SaveChanges();

            return Ok(new Message(1, "Cập nhật thông tin nhà hàng thành công"));
        }

        [HttpPost]
        [Route("api/XoaNhaHang")]
        public IHttpActionResult XoaNhaHang(int MaNH)
        {
            NHAHANG _model = db.NHAHANGs.SingleOrDefault(x => x.MaNH == MaNH);
            if (_model == null)
            {
                return Ok(new Message(0, "Nhà hàng không tồn tại"));
            }
            _model.isDelete = true;
            db.NHAHANGs.AddOrUpdate(_model);
            db.SaveChanges();

            List<NHAHANG> arrNH = db.NHAHANGs.Where(x=>x.isDelete == false).ToList();
            return Ok(arrNH);
        }

        [HttpPost]
        [Route("api/ThemNhaHang")]
        public IHttpActionResult ThemNhaHang(NHAHANG nhahang)
        {
            TAIKHOAN taikhoan = db.TAIKHOANs.SingleOrDefault(x => x.TenTK == nhahang.TenTK);
            taikhoan.VaiTro = "chunhahang";
            db.TAIKHOANs.AddOrUpdate(taikhoan);
            db.NHAHANGs.Add(nhahang);
            db.SaveChanges();
            return Ok(new Message(1, "Thêm nhà hàng thành công"));
        }

        [HttpPost]
        [Route("api/ThemMonAnTrongNhaHang")]
        public IHttpActionResult ThemMonAnTrongNhaHang(MONAN monan)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new Message(0, "Thêm món ăn mới thất bại"));
            }
            db.MONANs.Add(monan);
            db.SaveChanges();
            List<MONAN> arrMA = db.MONANs.Where(x => x.MaNH == monan.MaNH && x.isDelete == false).ToList();
            return Ok(arrMA);
        }

        [HttpPost]
        [Route("api/ChinhSuaMonAnTrongNhaHang")]
        public IHttpActionResult ChinhSuaMonAnTrongNhaHang(MONAN monan)
        {
            MONAN ma = db.MONANs.SingleOrDefault(x => x.MaMA == monan.MaMA);
            if (ma == null)
            {
                return Ok(new Message(0, "Mã món ăn không tồn tại"));
            }
            ma.TenMA = monan.TenMA;
            ma.Gia = monan.Gia;
            ma.HinhAnh = monan.HinhAnh;
            db.MONANs.AddOrUpdate(monan);
            db.SaveChanges();
            List<MONAN> arrMA = db.MONANs.Where(x => x.MaNH == ma.MaNH && x.MaNH == ma.MaNH && x.isDelete == false).ToList();
            return Ok(arrMA);
        }

        //ThongKe
        [HttpGet]
        [Route("api/GetTongDoanhThuCuaTungMonAnTheoNH")]
        public IHttpActionResult GetTongDoanhThuCuaTungMonAnTheoNH(int MaNH, DateTime ngaybatdau, DateTime ngayketthuc)
        {
            using (var ctx = new EateryEntities())
            {
                string startDate = DateTime.Parse(ngaybatdau.ToString()).ToString("yyyy-MM-dd");
                string endDate = DateTime.Parse(ngayketthuc.ToString()).ToString("yyyy-MM-dd");
                var arr = ctx.Database.SqlQuery<ThongKeModel>("SELECT dhct.MaMA, ma.TenMA, ma.HinhAnh, sum(DonGia) as TongDoanhThu FROM DONHANG dh join DONHANGCHITIET dhct on dh.MaDonHang = dhct.MaDHCT join MOnAn ma on ma.MaMA = dhct.MaMA " +
                    "Where dh.NgayMua between @ngaybatdau and @ngayketthuc and dh.MaNH = @MaNH and dh.TrangThaiDH >= 1 group by dhct.MaMA, ma.TenMA, ma.HinhAnh", new SqlParameter("@MaNH",MaNH), new SqlParameter("@ngaybatdau", ngaybatdau),new SqlParameter("@ngayketthuc", ngayketthuc)).ToList();
                return Ok(arr);
            }
        }
    } 
}
