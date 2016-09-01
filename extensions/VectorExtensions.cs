using System;
using UnityEngine;

public static class VectorExtensions
{
    [Flags]
    public enum VectorAxesMask
    {
        None = 0,
        X = 1,
        Y = 2,
        Z = 4,
        XY = X | Y,
        XZ = X | Z,
        YZ = Y | Z,
        XYZ = X | Y | Z
    }

    public static Vector2 SetValues(this Vector2 vector, Vector2 values, VectorAxesMask vectorAxesMask)
    {
        if ((vectorAxesMask & VectorAxesMask.X) != VectorAxesMask.None)
        {
            vector.x = values.x;
        }
        if ((vectorAxesMask & VectorAxesMask.Y) != VectorAxesMask.None)
        {
            vector.y = values.y;
        }
        return vector;
    }

    public static Vector2 SetValues(this Vector2 vector, float value, VectorAxesMask vectorAxesMask)
    {
        return vector.SetValues(new Vector2(value, value), vectorAxesMask);
    }

    public static Vector3 SetValues(this Vector3 vector, Vector3 values, VectorAxesMask vectorAxesMask)
    {
        if ((vectorAxesMask & VectorAxesMask.X) != VectorAxesMask.None)
        {
            vector.x = values.x;
        }
        if ((vectorAxesMask & VectorAxesMask.Y) != VectorAxesMask.None)
        {
            vector.y = values.y;
        }
        if ((vectorAxesMask & VectorAxesMask.Z) != VectorAxesMask.None)
        {
            vector.z = values.z;
        }
        return vector;
    }

    public static Vector3 SetValues(this Vector3 vector, float value, VectorAxesMask vectorAxesMask)
    {
        return vector.SetValues(new Vector3(value, value, value), vectorAxesMask);
    }

    public static Vector2 ToVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static Vector3 ToVector3(this Vector2 v)
    {
        return new Vector3(v.x, v.y, 0f);
    }

    public static Vector3 ToVector3(this Vector2 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }

    public static Vector2 ClampMagnitude(this Vector2 vector, float min, float max)
    {
        var result = vector;
        var sqrMagnitude = vector.sqrMagnitude;
        var num = min * min;
        var num2 = max * max;
        if (sqrMagnitude < num)
        {
            result = vector.normalized * min;
        }
        else if (sqrMagnitude > num2)
        {
            result = vector.normalized * max;
        }
        return result;
    }

    public static Vector3 ClampMagnitude(this Vector3 vector, float min, float max)
    {
        var result = vector;
        var sqrMagnitude = vector.sqrMagnitude;
        var num = min * min;
        var num2 = max * max;
        if (sqrMagnitude < num)
        {
            result = vector.normalized * min;
        }
        else if (sqrMagnitude > num2)
        {
            result = vector.normalized * max;
        }
        return result;
    }

    public static float Distance2D(this Vector3 v, Vector3 other)
    {
        return Vector2.Distance(new Vector2(v.x, v.y), new Vector2(other.x, other.y));
    }

    public static float DistanceSqr2D(this Vector3 v, Vector3 other)
    {
        return (new Vector2(v.x, v.y) - new Vector2(other.x, other.y)).sqrMagnitude;
    }

    public static Vector2 Clamp(this Vector2 v, Vector2 min, Vector2 max)
    {
        return new Vector2(Mathf.Clamp(v.x, min.x, max.x), Mathf.Clamp(v.y, min.y, max.y));
    }

    public static Vector2 Clamp(this Vector2 v, Rect rect)
    {
        return new Vector2(Mathf.Clamp(v.x, rect.xMin, rect.xMax), Mathf.Clamp(v.y, rect.yMin, rect.yMax));
    }

    public static Vector2 Clamp(this Vector2 v, float xMin, float yMin, float xMax, float yMax)
    {
        return new Vector2(Mathf.Clamp(v.x, xMin, xMax), Mathf.Clamp(v.y, yMin, yMax));
    }

    public static Vector2 Rotate(this Vector2 v, float radian)
    {
        var num = Mathf.Sin(radian);
        var num2 = Mathf.Cos(radian);
        var x = v.x;
        var y = v.y;
        v.x = num2 * x - num * y;
        v.y = num * x + num2 * y;
        return v;
    }

    public static Vector2 MoveToward(this Vector2 v, Vector2 target, ref float speed, float maxSpeed, float accel, float deccel, float deltaTime, out bool finished)
    {
        var vector = target - v;
        var num = 1;
        var b = maxSpeed;
        var num2 = speed / deccel;
        var num3 = speed * num2 / 2f;
        if (num3 * num3 > vector.sqrMagnitude)
        {
            num = -1;
        }
        else
        {
            var magnitude = vector.magnitude;
            var num4 = Mathf.Sqrt(magnitude * 2f / deccel);
            b = num4 * deccel;
        }
        speed = Mathf.Clamp(speed + ((num != 1) ? (-deccel * deltaTime) : (accel * deltaTime)), 0f, Mathf.Min(maxSpeed, b));
        var b2 = vector.normalized * (speed * deltaTime);
        if (b2.sqrMagnitude >= vector.sqrMagnitude)
        {
            speed = 0f;
            v = target;
            finished = true;
            return v;
        }
        v += b2;
        finished = false;
        return v;
    }

    public static Vector3 MoveToward(this Vector3 v, Vector3 target, ref float speed, float maxSpeed, float accel, float deccel, float deltaTime, out bool finished)
    {
        var vector = target - v;
        var num = 1;
        var b = maxSpeed;
        var num2 = speed / deccel;
        var num3 = speed * num2 / 2f;
        if (num3 * num3 > vector.sqrMagnitude)
        {
            num = -1;
        }
        else
        {
            var magnitude = vector.magnitude;
            var num4 = Mathf.Sqrt(magnitude * 2f / deccel);
            b = num4 * deccel;
        }
        speed = Mathf.Clamp(speed + ((num != 1) ? (-deccel * deltaTime) : (accel * deltaTime)), 0f, Mathf.Min(maxSpeed, b));
        var b2 = vector.normalized * (speed * deltaTime);
        if (b2.sqrMagnitude >= vector.sqrMagnitude)
        {
            speed = 0f;
            v = target;
            finished = true;
            return v;
        }
        v += b2;
        finished = false;
        return v;
    }

    public static Vector2 MoveToward(this Vector2 v, Vector2 target, ref Vector2 speed, float maxSpeed, float accel, float deltatime, out bool finished)
    {
        var a = target - v;
        a.Normalize();
        speed += a * accel * deltatime;
        if (speed.sqrMagnitude > maxSpeed * maxSpeed)
        {
            speed = speed.normalized * maxSpeed;
        }
        var vector = v + speed * deltatime;
        Vector3 v2 = speed;
        var f = Vector2.Dot(v2, (target - v).normalized);
        var f2 = Vector2.Dot(v2, (target - vector).normalized);
        finished = (Mathf.Sign(f) != Mathf.Sign(f2));
        v = vector;
        return v;
    }
}
