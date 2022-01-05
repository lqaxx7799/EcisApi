using EcisApi.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class VerificationConfirmUpdateDTO
    {
        public int VerificationConfirmRequirementId { get; set; }
        public string DocumentContent { get; set; }
        public string DocumentUrl { get; set; }
        public string DocumentType { get; set; }
        public long DocumentSize { get; set; }
        public string DocumentName { get; set; }

        public VerificationConfirmDocument[] VerificationConfirmDocuments { get; set; }
    }

    public class VerificationConfirmUpdateDTOValidator : AbstractValidator<VerificationConfirmUpdateDTO>
    {
        public VerificationConfirmUpdateDTOValidator()
        {
            RuleFor(x => x.VerificationConfirmRequirementId)
                .NotNull()
                .WithMessage("Không được để trống mã yêu cầu")
                .GreaterThan(0)
                .WithMessage("Không được để trống mã yêu cầu");

            RuleFor(x => x.DocumentContent)
                .NotNull()
                .WithMessage("Không được để trống nội dung");
        }
    }
}
