namespace Utilities
{
    public class UserInfoHelper
    {
        public static string UserId { get; set; }
        public static string LoginName { get; set; }
        public static string AccountName { get; set; }
        public static string Password { get; set; }
        public static string MAC { get; set; }
        public static string IP { get; set; }
        public static string InstitudId { get; set; }
        /// <summary>
        /// 是否已经登录
        /// </summary>
        public static bool IsHaveLogin { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public static bool IsLock { get; set; }
    }
}
