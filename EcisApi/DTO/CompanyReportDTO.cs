using EcisApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class CompanyReportDTO
    {
        public string ActionTitle { get; set; }
        public string Description { get; set; }
        public int? CompanyReportTypeId { get; set; }
        public int CreatorCompanyId { get; set; }
        public int TargetedCompanyId { get; set; }
        public int? VerificationProcessId { get; set; }
        public CompanyReportDocument[] CompanyReportDocuments { get; set; }
    }

    public class CompanyReportDTOValidator: AbstractValidator<CompanyReportDTO>
    {
        public CompanyReportDTOValidator()
        {
            RuleFor(x => x.ActionTitle)
                .NotEmpty()
                .WithMessage("Không được để trống tiêu đề");

            RuleFor(x => x.CreatorCompanyId)
                .GreaterThan(0)
                .WithMessage("Không được để trống mã doanh nghiệp");

            RuleFor(x => x.TargetedCompanyId)
               .GreaterThan(0)
               .WithMessage("Không được để trống mã doanh nghiệp");
        }
    }
}
