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
    public class NhaHangYeuThichController : ApiController
    {
        private EateryEntities db = new EateryEntities();

        [HttpGet]
        [Route("api/GetAllNhaHangYeuThich")]
        public IHttpActionResult GetAllNhaHangYeuThich()
        {
            List<NHAHANGYEUTHICH> arr = db.NHAHANGYEUTHICHes.ToList();
            return Ok(arr);
        }

        //NhaHangYeuThichFM
        [HttpGet]
        [Route("api/GetAllNhaHangYeuThichCuaTaiKhoan")]
        public IHttpActionResult GetAllNhaHangYeuThichCuaTaiKhoan(String tenTK)
        {
            List<NHAHANGYEUTHICH> arr = db.NHAHANGYEUTHICHes.Where(x => x.TenTK == tenTK).ToList();
            List<InfoRestaurant> arrRes = new List<InfoRestaurant>();
            foreach (var item in arr)
            {
                NHAHANG _model = db.NHAHANGs.Where(x => x.MaNH == item.MaNH).FirstOrDefault();
                String tenNH = db.LoaiNHs.Where(p => p.MaLoaiNH == _model.MaLoaiNH).FirstOrDefault().TenLoaiNH;

                InfoRestaurant infoRestaurant = new InfoRestaurant();
                infoRestaurant.MaNH = _model.MaNH;
                infoRestaurant.TenNH = _model.TenNH;
                infoRestaurant.DiaChi = _model.DiaChi;
                infoRestaurant.HinhAnh = _model.HinhAnh;
                infoRestaurant.MoTa = _model.MoTa;
                infoRestaurant.DanhGia = _model.DanhGia;
                infoRestaurant.TenTK = tenTK;
                infoRestaurant.MaLoaiNH = _model.MaLoaiNH;
                infoRestaurant.TenLoaiNH = tenNH;
                arrRes.Add(infoRestaurant);
            }
            return Ok(arrRes);
        }

        //Thêm nhà hàng yêu thích NhaHangChiTietFM
        [HttpPost]
        [Route("api/ThemHuyNhaHangYeuThich")]
        public IHttpActionResult ThemHuyNhaHangYeuThich(NHAHANGYEUTHICH nhahangyeuthich)
        {
            NHAHANGYEUTHICH nhyt = db.NHAHANGYEUTHICHes.SingleOrDefault(x => x.TenTK == nhahangyeuthich.TenTK && x.MaNH == nhahangyeuthich.MaNH);
            if (nhyt != null)
            {
                db.NHAHANGYEUTHICHes.Remove(nhyt);
                db.SaveChanges();
                return Ok(new Message(2, "Bạn đã huỷ yêu thích nhà hàng"));
            }
            db.NHAHANGYEUTHICHes.Add(nhahangyeuthich);
            db.SaveChanges();

            return Ok(new Message(1, "Yêu thích nhà hàng thành công"));
        }

        //Auto hiện tym khi yêu thích
        [HttpGet]
        [Route("api/CheckNhaHangYeuThichCuaTaiKhoan")]
        public IHttpActionResult CheckNhaHangYeuThichCuaTaiKhoan(string TenTK, int MaNH)
        {
            NHAHANGYEUTHICH nhyt = db.NHAHANGYEUTHICHes.SingleOrDefault(x => x.TenTK == TenTK && x.MaNH == MaNH);
            if (nhyt != null)
            {
                return Ok(new Message(1, "Bạn đang yêu thích nhà hàng này"));
            }
            return Ok(new Message(0, "Bạn chưa yêu thích nhà hàng này"));

        }
    }
}
