namespace MyEmgu
{
    public enum SysStation
    {
        SI,
        MI,
        SIMI
    }

    public static class SystemConfig
    {
        public static SysStation st { set; get; } = SysStation.SI;
    }
}