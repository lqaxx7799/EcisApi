using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class AuthenticateRequestDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class AuthenticateRequestDTOValidator : AbstractValidator<AuthenticateRequestDTO>
    {
        public AuthenticateRequestDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
