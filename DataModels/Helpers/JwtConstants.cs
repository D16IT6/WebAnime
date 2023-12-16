
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
