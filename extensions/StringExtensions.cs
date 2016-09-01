public static class StringExtensions
{
    public static long LineCount(this string s)
    {
        var num = 1L;
        var num2 = 0;
        while ((num2 = s.IndexOf('\n', num2)) != -1)
        {
            num += 1L;
            num2++;
        }
        return num;
    }
}