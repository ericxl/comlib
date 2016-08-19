using UnityEngine;
using System.Linq;

public static class AnimatorExtensions
{
    public static bool HasParameterOfType(this Animator self, string name, AnimatorControllerParameterType type)
    {
        var parameters = self.parameters;
        return parameters.Any(currParam => currParam.type == type && currParam.name == name);
    }

    public static void UpdateBool(this Animator animator, string parameterName, bool value)
    {
        if (animator.HasParameterOfType(parameterName, AnimatorControllerParameterType.Bool))
            animator.SetBool(parameterName, value);
    }
    public static void UpdateFloat(this Animator animator, string parameterName, float value)
    {
        if (animator.HasParameterOfType(parameterName, AnimatorControllerParameterType.Float))
            animator.SetFloat(parameterName, value);
    }
    public static void UpdateInteger(this Animator animator, string parameterName, int value)
    {
        if (animator.HasParameterOfType(parameterName, AnimatorControllerParameterType.Int))
            animator.SetInteger(parameterName, value);
    }
}
