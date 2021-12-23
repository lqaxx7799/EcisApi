using EcisApi.DTO;
using EcisApi.Helpers;
using EcisApi.Models;
using EcisApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class V1Controller : ControllerBase
    {
        private readonly IV1Service v1Service;

        public V1Controller(IV1Service v1Service)
        {
            this.v1Service = v1Service;
        }

        /// <summary>
        /// Lấy thông tin tài khoản của bên thụ hưởng
        /// </summary>
        /// <returns></returns>
        [HttpGet("Me")]
        [V1Authorize]
        public ActionResult<PublicV1ThirdPartyDTO> GetSelfThirdParty()
        {
            try
            {
                var account = (Account)HttpContext.Items["Account"];
                var id = account.ThirdParty?.Id;
                if (id == null)
                {
                    return BadRequest(new { Message = "InvalidToken" });
                }
                var result = v1Service.GetById(id.Value);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả các doanh nghiệp đã đăng ký sử dụng hệ thống ECIS
        /// </summary>
        /// <returns></returns>
        [HttpGet("Companies")]
        [V1Authorize]
        public ActionResult<ICollection<PublicCompanyDTO>> GetCompanies()
        {
            try
            {
                var result = v1Service.GetCompanies();
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết doanh nghiệp theo id doanh nghiệp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Company/ById/{id}")]
        [V1Authorize]
        public ActionResult<PublicCompanyDTO> GetCompanyById([FromRoute] int id)
        {
            try
            {
                var result = v1Service.GetCompanyById(id);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết doanh nghiệp theo mã đăng ký hoạt động của doanh nghiệp
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet("Company/ByCode/{code}")]
        [V1Authorize]
        public ActionResult<PublicCompanyDTO> GetCompanyByCode([FromRoute] string code)
        {
            try
            {
                var result = v1Service.GetCompanyByCode(code);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Lấy báo cáo phân loại theo tháng
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        [HttpGet("Report/ByTime")]
        [V1Authorize]
        public ActionResult<ICollection<PublicCompanyTypeModificationDTO>> GetModificationReport([FromQuery] int? month, [FromQuery] int? year)
        {
            try
            {
                var result = v1Service.GetModificationReport(month.GetValueOrDefault(DateTime.Now.Month), year.GetValueOrDefault(DateTime.Now.Year));
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Lấy báo cáo phân loại theo id doanh nghiệp
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Report/ByCompanyId/{id}")]
        [V1Authorize]
        public ActionResult<ICollection<PublicCompanyTypeModificationDTO>> GetModificationReportByCompanyId([FromRoute] int id)
        {
            try
            {
                var result = v1Service.GetModificationReportByCompanyId(id);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Lấy access token để sử dụng ECIS API V1
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("Authenticate")]
        public ActionResult<PublicV1AuthenticateResponseDTO> Authenticate([FromBody] PublicV1AuthenticateDTO payload)
        {
            try
            {
                var result = v1Service.Authenticate(payload);
                return Ok(result);
            }
            catch (BadHttpRequestException e)
            {
                return Unauthorized(new { e.Message });
            }
        }
    }
}
