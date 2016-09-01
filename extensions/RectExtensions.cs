using UnityEngine;

public static class RectExtensions
{
    public static Rect Merge(this Rect src, Rect mergeWith)
    {
        return new Rect
        {
            xMin = Mathf.Min(src.xMin, mergeWith.xMin),
            xMax = Mathf.Max(src.xMax, mergeWith.xMax),
            yMin = Mathf.Min(src.yMin, mergeWith.yMin),
            yMax = Mathf.Max(src.yMax, mergeWith.yMax)
        };
    }

    public static Rect Merge(this Rect src, ref Rect mergeWith)
    {
        return new Rect
        {
            xMin = Mathf.Min(src.xMin, mergeWith.xMin),
            xMax = Mathf.Max(src.xMax, mergeWith.xMax),
            yMin = Mathf.Min(src.yMin, mergeWith.yMin),
            yMax = Mathf.Max(src.yMax, mergeWith.yMax)
        };
    }

    public static bool Contains(this Rect src, Rect rect)
    {
        return rect.xMin > src.xMin && rect.xMax < src.xMax && rect.yMin > src.yMin && rect.yMax < src.yMax;
    }

    public static bool Contains(this Rect src, ref Rect rect)
    {
        return rect.xMin > src.xMin && rect.xMax < src.xMax && rect.yMin > src.yMin && rect.yMax < src.yMax;
    }

    public static float Volume(this Rect src)
    {
        return src.width * src.height;
    }

    public static bool Intersects(this Rect src, Rect intersectsWith)
    {
        return src.xMax >= intersectsWith.xMin && src.xMin <= intersectsWith.xMax && src.yMax >= intersectsWith.yMin && src.yMin <= intersectsWith.yMax;
    }

    public static bool Intersects(this Rect src, ref Rect intersectsWith)
    {
        return src.xMax >= intersectsWith.xMin && src.xMin <= intersectsWith.xMax && src.yMax >= intersectsWith.yMin && src.yMin <= intersectsWith.yMax;
    }

    public static bool Contains(this Rect src, Vector2 point)
    {
        return src.xMax >= point.x && src.xMin <= point.x && src.yMax >= point.y && src.yMin <= point.y;
    }

    public static bool Contains(this Rect src, ref Vector2 point)
    {
        return src.xMax >= point.x && src.xMin <= point.x && src.yMax >= point.y && src.yMin <= point.y;
    }

    public static Vector2 Restrict(this Rect src, ref Vector2 point)
    {
        return new Vector2(Mathf.Clamp(point.x, src.xMin, src.xMax), Mathf.Clamp(point.y, src.yMin, src.yMax));
    }

    public static Rect Inflate(this Rect src, Vector2 amount)
    {
        return new Rect(src.xMin - amount.x, src.yMin - amount.y, Mathf.Max(src.width + amount.x * 2f, 0f), Mathf.Max(src.height + amount.y * 2f, 0f));
    }

    public static Vector3 Restrict(this Rect src, ref Vector3 point)
    {
        return new Vector3(Mathf.Clamp(point.x, src.xMin, src.xMax), Mathf.Clamp(point.y, src.yMin, src.yMax), point.z);
    }
}