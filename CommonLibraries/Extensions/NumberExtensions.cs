public static class NumberExtensions
{
    public static bool ToBoolean(this int sourse)
    {
        return sourse != 0;
    }

    public static int ToInt(this bool sourse)
    {
        return sourse ? 1 : 0;
    }
}
