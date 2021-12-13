using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Helpers
{
    public class AppConstants
    {
        public static readonly string[] AllowedExtensions = { "jpg", "jpeg", "png", "doc", "docx", "xls", "xlsx", "pdf" };
        public static readonly long MaxFileSize = 25 * 1024 * 1024;

        public static class VerificationProcessSubmitMethod
        {
            public static readonly string ByCustomer = "BY_CUSTOMER";
            public static readonly string ByAgent = "BY_AGENT";
        }

        public static class VerificationProcessStatus
        {
            public static readonly string InProgress = "IN_PROGRESS";
            public static readonly string Submitted = "SUBMITTED";
            public static readonly string Reviewed = "REVIEWED";
            public static readonly string Classified = "CLASSIFIED";
            public static readonly string Finished = "FINISHED";
        }

        public static class VerificationCriteriaStatus
        {
            public static readonly string PENDING = "PENDING";
            public static readonly string VERIFIED = "VERIFIED";
            public static readonly string REJECTED = "REJECTED";

        }
        public static class CompanyModificationType
        {
            public static readonly string VERIFICATION = "VERIFICATION";
            public static readonly string REPORT = "REPORT";
            public static readonly string VIOLATION = "VIOLATION";
        }

        public static class ViolationReportStatus
        {
            public static readonly string PENDING = "PENDING";
            public static readonly string APPROVED = "APPROVED";
            public static readonly string REJECTED = "REJECTED";
        }

        public static class CompanyReportStatus
        {
            public static readonly string PENDING = "PENDING";
            public static readonly string APPROVED = "APPROVED";
            public static readonly string REJECTED = "REJECTED";
        }
    }
}
