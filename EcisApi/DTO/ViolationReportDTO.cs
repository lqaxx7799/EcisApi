using EcisApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class ViolationReportDTO
    {
        public string Description { get; set; }
        public int CompanyId { get; set; }
        public int ReportAgentId { get; set; }
        public ViolationReportDocument[] ViolationReportDocuments { get; set; }
    }

    public class ViolationReportDTOValidator : AbstractValidator<ViolationReportDTO> 
    { 
        public ViolationReportDTOValidator()
        {
            RuleFor(x => x.CompanyId)
                .NotNull()
                .WithMessage("Không được để trống mã doanh nghiệp")
                .GreaterThan(0)
                .WithMessage("Không được để trống mã doanh nghiệp");

            RuleFor(x => x.ReportAgentId)
                .NotNull()
                .WithMessage("Không được để trống mã cán bộ")
                .GreaterThan(0)
                .WithMessage("Không được để trống mã cán bộ");
        }
    }
}
