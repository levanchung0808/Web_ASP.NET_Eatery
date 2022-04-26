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
    public class TaiKhoanController : ApiController
    {
        private EateryEntities db = new EateryEntities();

        [HttpGet]
        [Route("api/taikhoan")]
        public IHttpActionResult GetAllTaiKhoan()
        {
            List<TAIKHOAN> arr = db.TAIKHOANs.ToList();
            return Ok(arr);
        }

        [HttpGet]
        [Route("api/taikhoan")]
        public IHttpActionResult GetTaiKhoanTheoTenTK(String TenTK)
        {
            TAIKHOAN _model = db.TAIKHOANs.Where(x => x.TenTK == TenTK).FirstOrDefault();
            if(_model == null)
            {
                return Ok(new Message(0, "Tài khoản không tồn tại"));
            }
            return Ok(_model);
        }

        [HttpGet]
        [Route("api/GetAllTaiKhoanChuaXoa")]
        public IHttpActionResult GetAllTaiKhoanChuaXoa(string TenTK)
        {
            List<TAIKHOAN> arr = db.TAIKHOANs.Where(x => x.isDelete == false && x.TenTK != TenTK).ToList();
            return Ok(arr);
        }

        [HttpPost]
        [Route("api/dangnhap")]
        public IHttpActionResult DangNhap(string TenTK, string MatKhau)
        {

            TAIKHOAN _taikhoan = db.TAIKHOANs.SingleOrDefault(x => x.TenTK.Equals(TenTK) && x.MatKhau.Equals(MatKhau) && x.isDelete == false);
            if (_taikhoan == null)
            {
                return Ok(new Message(0, "Đăng nhập thất bại. Vui lòng kiểm tra và thử lại"));
            }
            if (_taikhoan.VaiTro == "admin" )
            {
                return Ok(new Message(11, "Đăng nhập thành công với vai trò quản trị viên"));
            } else if(_taikhoan.VaiTro == "chucuahang")
            {
                NHAHANG _nhahang = db.NHAHANGs.SingleOrDefault(x => x.TenTK == TenTK);
                if(_nhahang.isDelete == false)
                {
                    return Ok(new Message(12, "Đăng nhập thành công với vai trò chủ cửa hàng"));
                }
            }else if(_taikhoan.VaiTro == "user")
            {
                return Ok(new Message(13, "Đăng nhập thành công với vai trò người dùng"));
            }
            return  Ok(new Message(0, "Đăng nhập thất bại. Vui lòng kiểm tra và thử lại"));
        }

        [HttpPost]
        [Route("api/dangky")]
        public IHttpActionResult DangKyTaiKhoan(TAIKHOAN taikhoan)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new Message(0, "Thêm tài khoản thất bại"));
            }
            TAIKHOAN tk = db.TAIKHOANs.SingleOrDefault(x=>x.TenTK == taikhoan.TenTK);
            if(tk != null)
            {
                return Ok(new Message(0, "Tài khoản đã tồn tại trong hệ thống"));
            }

            db.TAIKHOANs.AddOrUpdate(taikhoan);
            db.SaveChanges();
            return Ok(new Message(1, "Bạn đã đăng ký tài khoản thành công"));
        }

        [HttpGet]
        [Route("api/doimatkhau")]
        public IHttpActionResult DoiMatKhau(String TenTK, String MatKhauCu, String MatKhauMoi)
        {
            if (!ModelState.IsValid)
            {
                return Ok(new Message(0, "Đổi mật khẩu thất bại."));
            }

            TAIKHOAN taikhoanUpdate = db.TAIKHOANs.SingleOrDefault(x => x.TenTK.Equals(TenTK));

            if (taikhoanUpdate == null)
            {
                return Ok(new Message(2, "Tài khoản không tồn tại. Vui lòng kiểm tra và thử lại"));
            }

            if (!taikhoanUpdate.MatKhau.Equals(MatKhauCu))
            {
                return Ok(new Message(2, "Mật khẩu cũ không đúng. Vui lòng kiểm tra và thử lại"));
            }

            taikhoanUpdate.MatKhau = MatKhauMoi;
            db.TAIKHOANs.AddOrUpdate(taikhoanUpdate);
            db.SaveChanges();
            return Ok(new Message(1, "Đổi mật khẩu thành công"));
        }

        [HttpPost]
        [Route("api/capnhatthongtintaikhoan")]
        public IHttpActionResult CapNhatThongTinTaiKhoan(string TenTK, string HoTen, string SDT, string DiaChi)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Ok(new Message(0, "Cập nhật thông tin tài khoản thất bại"));
                }

                TAIKHOAN model = db.TAIKHOANs.SingleOrDefault(p => p.TenTK == TenTK);
                if (model == null)
                {
                    return Ok(new Message(0, "Cập nhật thông tin tài khoản thất bại"));
                }
                model.HoTen = HoTen;
                model.SDT = SDT;
                model.DiaChi = DiaChi;
                db.TAIKHOANs.AddOrUpdate(model);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return Ok(new Message(0, "Cập nhật thông tin tài khoản thất bại"));
            }
            return Ok(new Message(1, "Cập nhật thông tin tài khoản thành công."));
        }

        [HttpPost]
        [Route("api/XoaTaiKhoan")]
        public IHttpActionResult XoaTaiKhoan(string TenTKHT, string TenTK)
        {
            TAIKHOAN _model = db.TAIKHOANs.SingleOrDefault(x => x.TenTK == TenTK);
            if (_model == null)
            {
                return Ok(new Message(0, "Tài khoản không tồn tại"));
            }
            _model.isDelete = true;
            db.TAIKHOANs.AddOrUpdate(_model);
            db.SaveChanges();

            List<TAIKHOAN> arrTK = db.TAIKHOANs.Where(x=>x.isDelete == false && x.TenTK != TenTKHT).ToList();
            return Ok(arrTK);
        }

        [HttpGet]
        [Route("api/GetAllTaiKhoanChuaCoNhaHang")]
        public IHttpActionResult GetAllTaiKhoanChuaCoNhaHang()
        {
            List<TAIKHOAN> arrTK = db.TAIKHOANs.Where(x => x.VaiTro == "user" && x.isDelete == false).ToList();
            return Ok(arrTK);
        }

    }
}
