using UnityEngine;

public static class CameraExtensions
{
    public static bool WorldRectInView(this Camera camera, Rect worldRect, float depth)
    {
        return worldRect.Intersects(camera.GetRect(depth));
    }

    public static bool WorldRectInView(this Camera camera, Rect worldRect)
    {
        return camera.WorldRectInView(worldRect, 0f);
    }

    public static bool PointInView(this Camera camera, Vector3 point)
    {
        return camera.GetRect(0f).Contains(point);
    }

    public static Rect GetRect(this Camera camera, float depth)
    {
        var z = depth - camera.transform.position.z;
        Vector2 vector = camera.ViewportToWorldPoint(new Vector3(0f, 0f, z));
        Vector2 vector2 = camera.ViewportToWorldPoint(new Vector3(1f, 1f, z));
        return Rect.MinMaxRect(vector.x, vector.y, vector2.x, vector2.y);
    }
}
