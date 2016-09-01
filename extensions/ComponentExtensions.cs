using System.Collections.Generic;
using UnityEngine;

public static class ComponentExtensions
{
    public static void SpreadLocalPosition<T>(this IList<T> components, Vector3 position, float width, TextAlignment alignements) where T : Component
    {
        switch (alignements)
        {
            case TextAlignment.Left:
                for (var i = 0; i < components.Count; i++)
                {
                    var t = components[i];
                    t.transform.SetLocalPosition(position + new Vector3((float)i * width, 0f, 0f), VectorExtensions.VectorAxesMask.XYZ);
                }
                break;
            case TextAlignment.Center:
                for (var j = 0; j < components.Count; j++)
                {
                    var t2 = components[j];
                    t2.transform.SetLocalPosition(position + new Vector3(((float)j - (float)components.Count * 0.5f) * width + width * 0.5f, 0f, 0f), VectorExtensions.VectorAxesMask.XYZ);
                }
                break;
            case TextAlignment.Right:
                for (var k = 0; k < components.Count; k++)
                {
                    var t3 = components[k];
                    t3.transform.SetLocalPosition(position + new Vector3((float)(k - components.Count - 1) * width, 0f, 0f), VectorExtensions.VectorAxesMask.XYZ);
                }
                break;
        }
    }
}