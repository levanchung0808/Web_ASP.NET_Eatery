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
    public class KhuyenMaiController : ApiController
    {
        private EateryEntities db = new EateryEntities();

        [HttpGet]
        [Route("api/khuyenmai")]
        public IHttpActionResult GetAllKhuyenMai()
        {
            List<KHUYENMAI> arr = db.KHUYENMAIs.ToList();   
            return Ok(arr);
        }

        [HttpGet]
        [Route("api/khuyenmai/{id}")]
        public IHttpActionResult GetKhuyenMaiByID(String id)
        {
            KHUYENMAI _model = db.KHUYENMAIs.Where(x => x.MaKM == id).FirstOrDefault();
            return Ok(_model);
        }


        [HttpGet]
        [Route("api/GetAllMaKhuyenMaiTheoNH")]
        public IHttpActionResult GetAllMaKhuyenMaiTheoNH(int MaNH)
        {
            List<KHUYENMAI> arrKM = db.KHUYENMAIs.Where(x => x.MaNH == MaNH).ToList();
            return Ok(arrKM);
        }

        [HttpPost]
        [Route("api/ThemMaKhuyenMai")]
        public IHttpActionResult ThemMaKhuyenMai(KHUYENMAI khuyenmai)
        {
            KHUYENMAI km = db.KHUYENMAIs.SingleOrDefault(x => x.MaKM == khuyenmai.MaKM);
            if(km != null)
            {
                return Ok(new Message(0, "Mã khuyến mãi đã tồn tại, vui lòng nhập mã khác"));
            }
            db.KHUYENMAIs.Add(khuyenmai);
            db.SaveChanges();

            List<KHUYENMAI> arrKM = db.KHUYENMAIs.Where(x=>x.MaNH == khuyenmai.MaNH).ToList();
            return Ok(arrKM);
        }

        [HttpPost]
        [Route("api/ChinhSuaMaKhuyenMaiTheoNH")]
        public IHttpActionResult ChinhSuaMaKhuyenMaiTheoNH(KHUYENMAI khuyenmai)
        {
            KHUYENMAI km = db.KHUYENMAIs.SingleOrDefault(x=>x.MaNH == khuyenmai.MaNH && x.MaKM == khuyenmai.MaKM);
            if(km == null)
            {
                return Ok(new Message(0, "Mã khuyến mãi không tồn tại"));
            }
            km.TenKhuyenMai = khuyenmai.TenKhuyenMai;
            km.TienKM = khuyenmai.TienKM;
            km.SL = khuyenmai.SL;
            db.KHUYENMAIs.AddOrUpdate(km);
            db.SaveChanges();
            List<KHUYENMAI> arrKM = db.KHUYENMAIs.Where(x => x.MaNH == khuyenmai.MaNH).ToList();
            return Ok(arrKM);
        }

        [HttpPost]
        [Route("api/XoaMaKhuyenMai")]
        public IHttpActionResult XoaMaKhuyenMai(string MaKM)
        {
            KHUYENMAI km = db.KHUYENMAIs.SingleOrDefault(x => x.MaKM == MaKM);
            if (km == null)
            {
                return Ok(new Message(0, "Mã khuyến mãi không tồn tại"));
            }
            db.KHUYENMAIs.Remove(km);
            db.SaveChanges();
            List<KHUYENMAI> arrKM = db.KHUYENMAIs.Where(x => x.MaNH == km.MaNH).ToList();
            return Ok(arrKM);
        }
    }
}
