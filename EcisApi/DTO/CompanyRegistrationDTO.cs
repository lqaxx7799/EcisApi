using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class CompanyRegistrationDTO
    {
        public string Email { get; set; }
        public string CompanyNameVI { get; set; }
        public string CompanyNameEN { get; set; }
        public string CompanyCode { get; set; }
        public int ProvinceId { get; set; }
        public string LogoUrl { get; set; }
    }

    public class CompanyRegistrationDTOValidator : AbstractValidator<CompanyRegistrationDTO>
    {
        public CompanyRegistrationDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Không được để trống email")
                .EmailAddress()
                .WithMessage("Sai định dạng email");

            RuleFor(x => x.CompanyNameVI)
                .NotEmpty()
                .WithMessage("Không được để trống tên doanh nghiệp tiếng Việt");

            RuleFor(x => x.CompanyCode)
                .NotEmpty()
                .WithMessage("Không được để trống mã doanh nghiệp");

            RuleFor(x => x.ProvinceId)
                .NotEmpty()
                .WithMessage("Không được để trống tỉnh hoạt động");
        }
    }
}
