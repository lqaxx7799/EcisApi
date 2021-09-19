using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class ChangePasswordDTO
    {
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string ReenterNewPassword { get; set; }
    }

    public class ChangePasswordDTOValidator : AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordDTOValidator()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty()
                .WithMessage("Không được để trống mật khẩu hiện tại");

            RuleFor(x => x.NewPassword)
                .NotEmpty()
                .WithMessage("Không được để trống mật khẩu mới");

            RuleFor(x => x.ReenterNewPassword)
                .NotEmpty()
                .WithMessage("Không được để trống nhập lại mật khẩu mới")
                .Must((model, field) => model.NewPassword == field)
                .WithMessage("Vui lòng nhập lại khớp mật khẩu mới");
        }
    }
}
