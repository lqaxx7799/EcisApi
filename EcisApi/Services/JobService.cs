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
        protected readonly ISystemConfigurationRepository systemConfigurationRepository;
        protected readonly IVerificationProcessRepository verificationProcessRepository;

        protected readonly IVerificationProcessService verificationProcessService;

        protected readonly IEmailHelper emailHelper;
        protected readonly ILogger<JobService> logger;


        public JobService(
            ICompanyRepository companyRepository,
            ISystemConfigurationRepository systemConfigurationRepository,
            IVerificationProcessRepository verificationProcessRepository,

            IVerificationProcessService verificationProcessService,

            IEmailHelper emailHelper,
            ILogger<JobService> logger
            )
        {
            this.companyRepository = companyRepository;
            this.systemConfigurationRepository = systemConfigurationRepository;
            this.verificationProcessRepository = verificationProcessRepository;

            this.verificationProcessService = verificationProcessService;

            this.emailHelper = emailHelper;
            this.logger = logger;
        }

        public async Task CheckGenerateVerification()
        {
            Console.WriteLine($"Job CheckGenerateVerification Started");

            var companies = companyRepository.GetAllActivated();
            var durationConfig = systemConfigurationRepository.GetByKey(ConfigurationKeys.MODIFICATION_VALID_DURATION);
            var duration = durationConfig != null ? durationConfig.ConfigurationValue : "1-year";
            var durationValues = duration.Split("-");
            var durationTime = Convert.ToInt32(durationValues[0]);
            var durationType = durationValues[1];

            var processed = 0;

            foreach (var company in companies)
            {
                processed += 1;
                if (processed % 20 == 0)
                {
                    Console.WriteLine($"Job CheckGenerateVerification Processed {processed} in {companies.Count}");
                }

                var lastVerification = verificationProcessRepository.GetLatestByCompanyId(company.Id);
                if (lastVerification == null)
                {
                    await verificationProcessService.GenerateAsync(company.Id);
                    Console.WriteLine($"Generate verification success for company {company.Id} at: {DateTimeOffset.UtcNow}");
                    return;
                }
                if (
                    lastVerification.Status == AppConstants.VerificationProcessStatus.Finished
                    && DateTime.Today > lastVerification.FinishedAt.GetValueOrDefault(DateTime.Now).AddByType(durationTime, durationType))
                {
                    await verificationProcessService.GenerateAsync(company.Id);
                    Console.WriteLine($"Generate verification success for company {company.Id} at: {DateTimeOffset.UtcNow}");
                }
            }

            Console.WriteLine($"Job CheckGenerateVerification Finished {processed} in {companies.Count}");
        }

        public async Task CheckVerificationDeadline()
        {
            Console.WriteLine($"Job CheckVerificationDeadline Started");

            var unfinishedVerifications = verificationProcessRepository.Find(x => !x.IsSubmitted && !x.IsDeleted && x.SubmitDeadline > DateTime.Now);

            var processed = 0;

            foreach (var verification in unfinishedVerifications)
            {
                processed += 1;
                if (processed % 20 == 0)
                {
                    Console.WriteLine($"Job CheckVerificationDeadline Processed {processed} in {unfinishedVerifications.Count}");
                }
                await verificationProcessService.SubmitProcessAsync(verification.Id);
                Console.WriteLine($"Submit verification pass deadline success for verification {verification.Id} at :{DateTimeOffset.UtcNow}");
            }

            Console.WriteLine($"Job CheckVerificationDeadline Finished {processed} in {unfinishedVerifications.Count}");
        }
    }
}
