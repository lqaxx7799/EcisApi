using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.DTO
{
    public class PublicV1AuthenticateDTO
    {
        public string Email { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }

    public class PublicV1AuthenticateDTOValidator: AbstractValidator<PublicV1AuthenticateDTO>
    {
        public PublicV1AuthenticateDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty();
            RuleFor(x => x.ClientId)
                .NotEmpty();
            RuleFor(x => x.ClientSecret)
                .NotEmpty();
        }
    }
}
