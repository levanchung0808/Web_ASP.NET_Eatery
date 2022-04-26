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
    public class MonAnController : ApiController
    {
        private EateryEntities db = new EateryEntities();

        [HttpGet]
        [Route("api/monan")]
        public IHttpActionResult GetAllMonAn()
        {
            List<MONAN> arr = db.MONANs.ToList();
            return Ok(arr);
        }

        [HttpGet]
        [Route("api/GetAllMonAnTheoNhaHang")]
        public IHttpActionResult GetAllMonAnTheoNhaHang(int MaNH)
        {
            List<MONAN> arrMonAn = db.MONANs.Where(x => x.MaNH == MaNH && x.isDelete == false).ToList();
            return Ok(arrMonAn);
        }

        [HttpGet]
        [Route("api/monantheomama")]
        public IHttpActionResult GetMonAnTheoMaMA(int MaMA)
        {
            MONAN monan = db.MONANs.SingleOrDefault(x => x.MaMA == MaMA);
            
            String TenNH = db.NHAHANGs.SingleOrDefault(x=>x.MaNH == monan.MaNH).TenNH;
            monan.NHAHANG = null;
            monan.TenNH = TenNH;

            return Ok(monan);
        }

        [HttpPost]
        [Route("api/XoaMonAn")]
        public IHttpActionResult XoaMonAn(int MaMA)
        {
            MONAN ma = db.MONANs.SingleOrDefault(x => x.MaMA == MaMA);
            if (ma == null)
            {
                return Ok(new Message(0, "Nhà hàng không tồn tại"));
            }
            ma.isDelete = true;
            db.MONANs.AddOrUpdate(ma);
            db.SaveChanges();

            List<MONAN> arrMA = db.MONANs.Where(x => x.isDelete == false && x.MaNH == ma.MaNH).ToList();
            return Ok(arrMA);
        }

    }
}
