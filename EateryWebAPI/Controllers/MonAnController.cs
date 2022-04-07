using EateryWebAPI.Models;
using System;
using System.Collections.Generic;
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
            List<MONAN> arrMonAn = db.MONANs.Where(x => x.MaNH == MaNH).ToList();
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

        ////[HttpGet]
        ////[Route("api/ThongKeDoanhThuMonAnTheoNH")]
        ////public IHttpActionResult ThongKeDoanhThuMonAnTheoNH(int MaNH)
        ////{
        ////    double DoanhThu = db.MONANs.SingleOrDefault(x=>x.)

        ////    String TenNH = db.NHAHANGs.SingleOrDefault(x => x.MaNH == monan.MaNH).TenNH;
        ////    monan.NHAHANG = null;
        ////    monan.TenNH = TenNH;

        ////    return Ok(monan);
        ////}
    }
}
