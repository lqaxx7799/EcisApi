using EcisApi.Helpers;
using EcisApi.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Services
{
    public interface IJobService
    {
        Task CheckGenerateVerification();
        Task CheckVerificationDeadline();
    }

    public class JobService : IJobService
    {
        protected readonly ICompanyRepository companyRepository;
        protected readonly IVerificationProcessRepository verificationProcessRepository;

        protected readonly IVerificationProcessService verificationProcessService;

        protected readonly IEmailHelper emailHelper;
        protected readonly ILogger<JobService> logger;


        public JobService(
            ICompanyRepository companyRepository,
            IVerificationProcessRepository verificationProcessRepository,

            IVerificationProcessService verificationProcessService,

            IEmailHelper emailHelper,
            ILogger<JobService> logger
            )
        {
            this.companyRepository = companyRepository;
            this.verificationProcessRepository = verificationProcessRepository;

            this.verificationProcessService = verificationProcessService;

            this.emailHelper = emailHelper;
            this.logger = logger;
        }

        public async Task CheckGenerateVerification()
        {
            var companies = companyRepository.GetAllActivated();
            foreach (var company in companies)
            {
                var lastVerification = verificationProcessService.GetCompanyLast(company.Id);

                if (lastVerification == null)
                {
                    await verificationProcessService.GenerateAsync(company.Id);
                    logger.LogInformation($"Generate verification success at :{DateTimeOffset.UtcNow}", new { 
                        companyId = company.Id
                    });
                    break;
                }
                if (DateTime.Today == lastVerification.CreatedAt.AddYears(1).Date)
                {
                    await verificationProcessService.GenerateAsync(company.Id);
                    logger.LogInformation($"Generate verification success at :{DateTimeOffset.UtcNow}", new
                    {
                        companyId = company.Id
                    });
                }
            }
        }

        public async Task CheckVerificationDeadline()
        {
            var unfinishedVerifications = verificationProcessRepository.Find(x => !x.IsSubmitted && !x.IsDeleted && x.SubmitDeadline > DateTime.Now);
            foreach (var verification in unfinishedVerifications)
            {
                await verificationProcessService.SubmitProcessAsync(verification.Id);
                logger.LogInformation($"submit verification pass deadline success at :{DateTimeOffset.UtcNow}", new
                {
                    verificationProcessId = verification.Id
                });
            }
        }
    }
}
