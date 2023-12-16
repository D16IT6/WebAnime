namespace DataModels.Helpers
{
    public static class AuthConstants
    {
        public static int MaxFailedAccessAttemptsBeforeLockout = 3;
        public static int LockoutMinutes = 1;


        public const string DpapiPassphrase = "WebAnime.DpapiDataProtectionProvider.TalonEzio.123!@#";

        public const string TokenProtectionKey = "WebAnime.MVC.ResetTokenKey.Abc!@#)(&";

    }
}
