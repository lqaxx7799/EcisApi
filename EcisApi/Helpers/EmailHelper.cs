using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace EcisApi.Helpers
{
    public interface IEmailHelper
    {
        Task SendEmailAsync(string[] recipients, string subject, string templateName, Dictionary<string, string> parameters);
    }

    public class EmailHelper : IEmailHelper
    {
        private readonly AppSettings _appSettings;

        public EmailHelper(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public async Task SendEmailAsync(string[] recipients, string subject, string templateName, Dictionary<string, string> parameters)
        {
            var content = InnerGetContent(templateName, parameters);
            await InnerSendEmail(recipients, subject, content);
        }

        private static bool IsDevelopment => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        private string InnerGetContent(string templateName, Dictionary<string, string> parameters)
        {
            try
            {
                string path;
                if (IsDevelopment)
                {
                    path = Path.Combine(
                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                        @$"Assets\EmailTemplates\{templateName}.html");
                }
                else
                {
                    path = @$"/app/Assets/EmailTemplates/{templateName}.html";
                }

                StreamReader str = new(path);
                string mailText = str.ReadToEnd();
                str.Close();

                foreach (var param in parameters)
                {
                    mailText = mailText.Replace($"[{param.Key}]", param.Value);
                }
                return mailText;
            }
            catch (Exception)
            {
                return "";
            }
        }

        private async Task InnerSendEmail(string[] recipients, string subject, string content)
        {
            var emailSender = _appSettings.EmailSender;
            var emailPassword = _appSettings.EmailPassword;
            var smtpServer = _appSettings.SmtpServer;
            var smtpPortNumber = _appSettings.SmtpPortNumber;

            MailMessage _mailmsg = new()
            {
                IsBodyHtml = true,
                From = new MailAddress(emailSender),
                Subject = subject,
                Body = content
            };
            Array.ForEach(recipients, recipient =>
            {
                _mailmsg.To.Add(recipient);
            });

            SmtpClient _smtp = new()
            {
                Host = smtpServer,
                Port = smtpPortNumber,
                EnableSsl = true
            };

            //Set Sender UserEmailID, Password  
            NetworkCredential _network = new(emailSender, emailPassword);
            _smtp.Credentials = _network;

            await _smtp.SendMailAsync(_mailmsg);
        }
    }

    public static class EmailTemplate
    {
        public static readonly string AgentCreated = "AgentCreated";
        public static readonly string CompanyRegistrationSuccess = "CompanyRegistrationSuccess";
        public static readonly string CompanyRegistrationVerified = "CompanyRegistrationVerified";
        public static readonly string ThirdPartyCreated = "ThirdPartyCreated";
        public static readonly string VerificationConfirmRequirementAnnounceAgent = "VerificationConfirmRequirementAnnounceAgent";
        public static readonly string VerificationConfirmRequirementAnnounceCompany = "VerificationConfirmRequirementAnnounceCompany";
        public static readonly string VerificationFinished = "VerificationFinished";
    }
}
