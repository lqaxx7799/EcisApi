using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class VerifyCompanyDTO
    {
        public int AccountId { get; set; }
    }

    public class VerifyCompanyDTOValidator : AbstractValidator<VerifyCompanyDTO>
    {
        public VerifyCompanyDTOValidator()
        {
            RuleFor(x => x.AccountId)
                .NotEmpty()
                .WithMessage("Không được để trống AccountId");
        }
    }
}
