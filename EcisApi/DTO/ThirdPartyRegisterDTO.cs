using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class ThirdPartyRegisterDTO
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class ThirdPartyRegisterDTOValidator : AbstractValidator<ThirdPartyRegisterDTO>
    {
        public ThirdPartyRegisterDTOValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Không được để trống tên người dùng");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Không được để trống email");
        }
    }
}
