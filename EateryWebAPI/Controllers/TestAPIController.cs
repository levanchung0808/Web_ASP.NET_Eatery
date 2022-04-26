using EateryWebAPI.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace EateryWebAPI.Controllers
{
    public class TestAPIController : ApiController
    {
        private EateryEntities db = new EateryEntities();
        [HttpGet]
        [Route("api/resetPassword")]
        public IHttpActionResult CapNhatMatKhau(string TenTK, string code, string matKhauMoi)
        {
            TAIKHOAN taikhoan = db.TAIKHOANs.SingleOrDefault(x => x.TenTK == TenTK && x.CodeForgotPassword == code);
            if (taikhoan == null)
            {
                return Ok(new Message(1, "Cập nhật mật khẩu thất bại. Vui lòng kiểm tra lại"));
            }
            taikhoan.MatKhau = matKhauMoi;
            taikhoan.CodeForgotPassword = null;
            db.TAIKHOANs.AddOrUpdate(taikhoan);
            db.SaveChanges();
            return Ok(new Message(1, "Cập nhật mật khẩu mới thành công"));

        }

        [HttpGet]
        [Route("api/getCodeResetPassword")]
        public IHttpActionResult sendPassEmail(string TenTK)
        {
            Random random = new Random();
            int _rd = 100000 + random.Next(900000);

            TAIKHOAN taikhoan = db.TAIKHOANs.SingleOrDefault(x => x.TenTK == TenTK);
            if (taikhoan == null)
            {
                return Ok(new Message(0, "Tài khoản không tồn tại. Vui lòng kiểm tra lại"));
            }
            taikhoan.CodeForgotPassword = _rd.ToString();
            db.TAIKHOANs.AddOrUpdate(taikhoan);
            db.SaveChanges();

            MailAddress fromAddress = new MailAddress("levanchung.it@gmail.com", "ADMIN EATERY");
            MailAddress toAddress = new MailAddress(TenTK);
            const string fromPassword = "rlew tfnk qlmg kpdm";
            string subject = "Bán đồ ăn Eatery - Gửi yêu cầu khôi phục mật khẩu";
            string body = "Mật mã khôi phục mật khẩu của bạn là: " + _rd;
            SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (MailMessage message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            })
            {
                smtp.Send(message);
                return Ok(new Message(1, "Mã đã được gửi về email " + TenTK));
            }
        }

        [HttpPost]
        [Route("api/CapNhatMatKhauCuaTK")]
        public IHttpActionResult CapNhatMatKhauCuaTK(string TenTK,string MatKhauCu, string MatKhauMoi, string NhapLaiMatKhauMoi)
        {
            TAIKHOAN taikhoan = db.TAIKHOANs.SingleOrDefault(x => x.TenTK == TenTK);
            if (taikhoan == null)
            {
                return Ok(new Message(0, "Cập nhật mật khẩu thất bại. Vui lòng kiểm tra lại"));
            }

            if(!taikhoan.MatKhau.Equals(MatKhauCu))
            {
                return Ok(new Message(0, "Mật khẩu cũ không đúng. Vui lòng kiểm tra lại"));
            }
            if (taikhoan.MatKhau.Equals(MatKhauMoi))
            {
                return Ok(new Message(0, "Mật khẩu mới giống mật khẩu cũ. Vui lòng nhập mật khẩu khác"));
            }
            if(MatKhauMoi != NhapLaiMatKhauMoi)
            {
                return Ok(new Message(0, "Mật khẩu phải giống nhau"));
            }
            taikhoan.MatKhau = MatKhauMoi;
            db.TAIKHOANs.AddOrUpdate(taikhoan);
            db.SaveChanges();
            return Ok(new Message(1, "Cập nhật mật khẩu mới thành công"));


        }

    }
}
