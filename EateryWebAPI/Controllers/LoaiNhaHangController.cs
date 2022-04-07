using EateryWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EateryWebAPI.Controllers
{
    public class LoaiNhaHangController : ApiController
    {
        private EateryEntities db = new EateryEntities();

        [HttpGet]
        [Route("api/GetAllLoaiNhaHang")]
        public IHttpActionResult GetAllLoaiNhaHang()
        {
            List<LoaiNH> arr = db.LoaiNHs.ToList();
            return Ok(arr);
        }

        [HttpGet]
        [Route("api/loainhahang/{id}")]
        public IHttpActionResult GetLoaiNhaHangByID(String id)
        {
            LoaiNH _model = db.LoaiNHs.Where(x => x.MaLoaiNH == id).FirstOrDefault();
            return Ok(_model);
        }
    }
}
