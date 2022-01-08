using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Models
{
    public class VerificationConfirmRequirement : BaseModel
    {
        public int Id { get; set; }
        public DateTime ScheduledTime { get; set; }
        public string ScheduledLocation { get; set; }
        public DateTime? AnnouncedAgentAt { get; set; }
        public DateTime? AnnouncedCompanyAt { get; set; }
        public DateTime? ConfirmedAt { get; set; }

        public string AnnounceAgentDocumentContent { get; set; }

        public string ConfirmDocumentContent { get; set; }

        public int? VerificationProcessId { get; set; }
        public int? AssignedAgentId { get; set; }
        public int? ConfirmCompanyTypeId { get; set; }
        public int? VerificationCriteriaId { get; set; }

        public virtual VerificationProcess VerificationProcess { get; set; }
        public virtual Agent AssignedAgent { get; set; }
        public virtual CompanyType ConfirmCompanyType { get; set; }
        public virtual VerificationCriteria VerificationCriteria { get; set; }

        public virtual ICollection<VerificationConfirmDocument> VerificationConfirmDocuments { get; set; }
    }

    public class VerificationConfirmRequirementValidator : AbstractValidator<VerificationConfirmRequirement>
    {
        public VerificationConfirmRequirementValidator()
        {
            RuleFor(x => x.AnnounceAgentDocumentContent)
                .NotEmpty()
                .WithMessage("Không được để trống nội dung yêu cầu");

            RuleFor(x => x.AssignedAgentId)
                .NotNull()
                .WithMessage("Không được để trống cán bộ xử lý")
                .GreaterThan(0)
                .WithMessage("Không được để trống cán bộ xử lý");

            RuleFor(x => x.VerificationCriteriaId)
                .NotNull()
                .WithMessage("Không được để trống tiêu chí")
                .GreaterThan(0)
                .WithMessage("Không được để trống tiêu chí");
        }
    }
}
