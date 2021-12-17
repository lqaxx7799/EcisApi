using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class AgentCreateDTO
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int[] ProvinceIds { get; set; }
    }

    public class AgentCreateDTOValidator : AbstractValidator<AgentCreateDTO>
    {
        public AgentCreateDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Không được để trống email");

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("Không được để trống tên");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Không được để trống họ");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("Không được để trống số điện thoại");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .WithMessage("Không được để trống ngày sinh");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Không được để trống địa chỉ");

            RuleFor(x => x.ProvinceIds)
                .NotEmpty()
                .WithMessage("Không được để trống danh sách tỉnh");
        }
    }
}
