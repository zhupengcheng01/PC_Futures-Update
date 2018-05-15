namespace Futures.Enum
{

    /// <summary>
    /// K线枚举
    /// </summary>
    public enum EnumBarType
    {
        Invaild = -1,        //无效
        BAR_TYPE_MIN1 = 1,   //1分钟
        BAR_TYPE_MIN3 = 3,   //3分钟
        BAR_TYPE_MIN5 = 5,   //5分钟
        BAR_TYPE_MIN15 = 15, //15分钟
        BAR_TYPE_MIN30 = 30, //30分钟
        BAR_TYPE_MIN60 = 60, //60分钟
        BAR_TYPE_HOUR2 = 62, //2hr
        BAR_TYPE_HOUR4 = 64, //4hr
        BAR_TYPE_DAY = 101,  //日线
        BAR_TYPE_WEEK = 102,  //周线
        BAR_TYPE_MONTH = 103,  //月线
        BAR_FS_MONTH = 200,//分时
        BAR_FS_OtherMin = 201//任意分钟
    }
}
