using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class TransformExtensions
{
    public static void SetPosition(this Transform transform, Vector3 position, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.position = transform.position.SetValues(position, vectorAxesMask);
    }

    public static void SetPosition(this Transform transform, float position, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.SetPosition(new Vector3(position, position, position), vectorAxesMask);
    }

    public static void SetLocalPosition(this Transform transform, Vector3 position, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.localPosition = transform.localPosition.SetValues(position, vectorAxesMask);
    }

    public static void SetLocalPosition(this Transform transform, float position, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.SetLocalPosition(new Vector3(position, position, position), vectorAxesMask);
    }

    public static void SetEulerAngles(this Transform transform, Vector3 angles, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.eulerAngles = transform.eulerAngles.SetValues(angles, vectorAxesMask);
    }

    public static void SetEulerAngles(this Transform transform, float angle, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.SetEulerAngles(new Vector3(angle, angle, angle), vectorAxesMask);
    }

    public static void SetLocalEulerAngles(this Transform transform, Vector3 angles, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.localEulerAngles = transform.localEulerAngles.SetValues(angles, vectorAxesMask);
    }

    public static void SetLocalEulerAngles(this Transform transform, float angle, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.SetLocalEulerAngles(new Vector3(angle, angle, angle), vectorAxesMask);
    }

    public static void SetLocalScale(this Transform transform, Vector3 scale, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.localScale = transform.localScale.SetValues(scale, vectorAxesMask);
    }

    public static void SetLocalScale(this Transform transform, float scale, VectorExtensions.VectorAxesMask vectorAxesMask = VectorExtensions.VectorAxesMask.XYZ)
    {
        transform.SetLocalScale(new Vector3(scale, scale, scale), vectorAxesMask);
    }

    public static Transform[] GetChildren(this Transform parent)
    {
        var array = new Transform[parent.childCount];
        for (var i = 0; i < parent.childCount; i++)
        {
            array[i] = parent.GetChild(i);
        }
        return array;
    }

    public static Transform[] GetChildrenRecursive(this Transform parent)
    {
        var list = new List<Transform>();
        var children = parent.GetChildren();
        for (var i = 0; i < children.Length; i++)
        {
            var transform = children[i];
            list.Add(transform);
            if (transform.childCount > 0)
            {
                list.AddRange(transform.GetChildrenRecursive());
            }
        }
        return list.ToArray();
    }

    public static Transform FindChild(this Transform parent, Predicate<Transform> predicate)
    {
        for (var i = 0; i < parent.childCount; i++)
        {
            var child = parent.GetChild(i);
            if (predicate(child))
            {
                return child;
            }
        }
        return null;
    }

    public static Transform FindChildRecursive(this Transform parent, string childName)
    {
        return parent.FindChildRecursive(child => child.name == childName);
    }

    public static Transform FindChildRecursive(this Transform parent, Predicate<Transform> predicate)
    {
        var childrenRecursive = parent.GetChildrenRecursive();
        for (var i = 0; i < childrenRecursive.Length; i++)
        {
            var transform = childrenRecursive[i];
            if (predicate(transform))
            {
                return transform;
            }
        }
        return null;
    }

    public static Transform[] FindChildren(this Transform parent, string childName)
    {
        return parent.FindChildren(child => child.name == childName);
    }

    public static Transform[] FindChildren(this Transform parent, Predicate<Transform> predicate)
    {
        var list = new List<Transform>();
        var children = parent.GetChildren();
        for (var i = 0; i < children.Length; i++)
        {
            var transform = children[i];
            if (predicate(transform))
            {
                list.Add(transform);
            }
        }
        return list.ToArray();
    }

    public static Transform[] FindChildrenRecursive(this Transform parent, string childName)
    {
        return parent.FindChildrenRecursive(child => child.name == childName);
    }

    public static Transform[] FindChildrenRecursive(this Transform parent, Predicate<Transform> predicate)
    {
        var list = new List<Transform>();
        var childrenRecursive = parent.GetChildrenRecursive();
        for (var i = 0; i < childrenRecursive.Length; i++)
        {
            var transform = childrenRecursive[i];
            if (predicate(transform))
            {
                list.Add(transform);
            }
        }
        return list.ToArray();
    }

}
