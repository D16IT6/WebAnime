using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Helpers
{
    public static class JwtConstants
    {
        public static string Issuer => "TalonEzio";
        public static string Audience => "All";
        public static int ExpriedAfterMinutes = 30;
        public static int ExpiredRefreshTokenMonths = 1;
    }
}
