using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcisApi.Helpers
{
    public class AppConstants
    {
        public static class VerificationProcessSubmitMethod
        {
            public static readonly string ByCustomer = "BY_CUSTOMER";
            public static readonly string ByAgent = "BY_AGENT";
        }

        public static class VerificationCriteriaStatus
        {
            public static readonly string PEDNING = "PENDING";
            public static readonly string VERIFIED = "VERIFIED";
            public static readonly string REJECTED = "REJECTED";
        }
    }
}
