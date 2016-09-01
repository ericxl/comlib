using System;
using UnityEngine;

public static class ColorExtensions
{
    [Flags]
    public enum ColorChannelMask
    {
        None = 0,
        R = 1,
        G = 2,
        B = 4,
        RG = 3,
        RB = 5,
        GB = 6,
        RGB = 7,
        A = 8,
        RA = 9,
        GA = 10,
        RGA = 11,
        BA = 12,
        RBA = 13,
        GBA = 14,
        RGBA = 15
    }

    public static Color SetValues(this Color color, Color values, ColorChannelMask colorChannelMask)
    {
        if ((colorChannelMask & ColorChannelMask.R) != ColorChannelMask.None)
        {
            color.r = values.r;
        }
        if ((colorChannelMask & ColorChannelMask.G) != ColorChannelMask.None)
        {
            color.g = values.g;
        }
        if ((colorChannelMask & ColorChannelMask.B) != ColorChannelMask.None)
        {
            color.b = values.b;
        }
        if ((colorChannelMask & ColorChannelMask.A) != ColorChannelMask.None)
        {
            color.a = values.a;
        }
        return color;
    }

    public static Color SetValues(this Color color, float value, ColorChannelMask colorChannelMask)
    {
        return color.SetValues(new Color(value, value, value, value), colorChannelMask);
    }
}
