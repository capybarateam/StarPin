using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

// easing functions from https://gist.github.com/Fonserbc/3d31a25e87fdaa541ddf

[ExecuteInEditMode]
public class EasingHelper : MonoBehaviour
{
    public AnimationClip Animation;
    public string PropertyName;
    public CurveTypeEnum CurveType;
    public CurveModeEnum CurveMode;
    public int NumKeyFrames = 10;
    public float Time = 2;
    public float TimeMax = 1.5f;
    public float ScalarA = 0.0f;
    public float ScalarB = 1.0f;

    public enum CurveTypeEnum
    {
        Linear,
        Quadratic,
        Cubic,
        Quartic,
        Quintic,
        Sinusoidal,
        Exponential,
        Back,
        Bounce,
        Elastic,
        Circular,

        Count,
    }

    public enum CurveModeEnum
    {
        In,
        Out,
        InOut,

        Count,
    }

    public readonly Func<float, float>[,] EasingFunctionList = new Func<float, float>[(int)CurveTypeEnum.Count, (int)CurveModeEnum.Count];

    public EasingHelper()
    {
        EasingFunctionList[(int)CurveTypeEnum.Linear, (int)CurveModeEnum.In] = EasingFunctions.Linear;
        EasingFunctionList[(int)CurveTypeEnum.Linear, (int)CurveModeEnum.Out] = EasingFunctions.Linear;
        EasingFunctionList[(int)CurveTypeEnum.Linear, (int)CurveModeEnum.InOut] = EasingFunctions.Linear;

        EasingFunctionList[(int)CurveTypeEnum.Quadratic, (int)CurveModeEnum.In] = EasingFunctions.Quadratic.In;
        EasingFunctionList[(int)CurveTypeEnum.Quadratic, (int)CurveModeEnum.Out] = EasingFunctions.Quadratic.Out;
        EasingFunctionList[(int)CurveTypeEnum.Quadratic, (int)CurveModeEnum.InOut] = EasingFunctions.Quadratic.InOut;

        EasingFunctionList[(int)CurveTypeEnum.Cubic, (int)CurveModeEnum.In] = EasingFunctions.Cubic.In;
        EasingFunctionList[(int)CurveTypeEnum.Cubic, (int)CurveModeEnum.Out] = EasingFunctions.Cubic.Out;
        EasingFunctionList[(int)CurveTypeEnum.Cubic, (int)CurveModeEnum.InOut] = EasingFunctions.Cubic.InOut;

        EasingFunctionList[(int)CurveTypeEnum.Quartic, (int)CurveModeEnum.In] = EasingFunctions.Quartic.In;
        EasingFunctionList[(int)CurveTypeEnum.Quartic, (int)CurveModeEnum.Out] = EasingFunctions.Quartic.Out;
        EasingFunctionList[(int)CurveTypeEnum.Quartic, (int)CurveModeEnum.InOut] = EasingFunctions.Quartic.InOut;

        EasingFunctionList[(int)CurveTypeEnum.Quintic, (int)CurveModeEnum.In] = EasingFunctions.Quintic.In;
        EasingFunctionList[(int)CurveTypeEnum.Quintic, (int)CurveModeEnum.Out] = EasingFunctions.Quintic.Out;
        EasingFunctionList[(int)CurveTypeEnum.Quintic, (int)CurveModeEnum.InOut] = EasingFunctions.Quintic.InOut;

        EasingFunctionList[(int)CurveTypeEnum.Sinusoidal, (int)CurveModeEnum.In] = EasingFunctions.Sinusoidal.In;
        EasingFunctionList[(int)CurveTypeEnum.Sinusoidal, (int)CurveModeEnum.Out] = EasingFunctions.Sinusoidal.Out;
        EasingFunctionList[(int)CurveTypeEnum.Sinusoidal, (int)CurveModeEnum.InOut] = EasingFunctions.Sinusoidal.InOut;

        EasingFunctionList[(int)CurveTypeEnum.Exponential, (int)CurveModeEnum.In] = EasingFunctions.Exponential.In;
        EasingFunctionList[(int)CurveTypeEnum.Exponential, (int)CurveModeEnum.Out] = EasingFunctions.Exponential.Out;
        EasingFunctionList[(int)CurveTypeEnum.Exponential, (int)CurveModeEnum.InOut] = EasingFunctions.Exponential.InOut;

        EasingFunctionList[(int)CurveTypeEnum.Back, (int)CurveModeEnum.In] = EasingFunctions.Back.In;
        EasingFunctionList[(int)CurveTypeEnum.Back, (int)CurveModeEnum.Out] = EasingFunctions.Back.Out;
        EasingFunctionList[(int)CurveTypeEnum.Back, (int)CurveModeEnum.InOut] = EasingFunctions.Back.InOut;

        EasingFunctionList[(int)CurveTypeEnum.Bounce, (int)CurveModeEnum.In] = EasingFunctions.Bounce.In;
        EasingFunctionList[(int)CurveTypeEnum.Bounce, (int)CurveModeEnum.Out] = EasingFunctions.Bounce.Out;
        EasingFunctionList[(int)CurveTypeEnum.Bounce, (int)CurveModeEnum.InOut] = EasingFunctions.Bounce.InOut;

        EasingFunctionList[(int)CurveTypeEnum.Elastic, (int)CurveModeEnum.In] = EasingFunctions.Elastic.In;
        EasingFunctionList[(int)CurveTypeEnum.Elastic, (int)CurveModeEnum.Out] = EasingFunctions.Elastic.Out;
        EasingFunctionList[(int)CurveTypeEnum.Elastic, (int)CurveModeEnum.InOut] = EasingFunctions.Elastic.InOut;

        EasingFunctionList[(int)CurveTypeEnum.Circular, (int)CurveModeEnum.In] = EasingFunctions.Circular.In;
        EasingFunctionList[(int)CurveTypeEnum.Circular, (int)CurveModeEnum.Out] = EasingFunctions.Circular.Out;
        EasingFunctionList[(int)CurveTypeEnum.Circular, (int)CurveModeEnum.InOut] = EasingFunctions.Circular.InOut;
    }

    public void GenerateCurve()
    {
        List<Keyframe> keys = new List<Keyframe>(NumKeyFrames);
        Func<float, float> function = EasingFunctionList[(int)CurveType, (int)CurveMode];

        for (int i = 0; i < NumKeyFrames; i++)
        {
            float timeFrac = (float)i / (NumKeyFrames - 1);
            float time = Time * timeFrac;
            float value = Mathf.Lerp(ScalarA, ScalarB, function(timeFrac));

            if (time <= TimeMax)
            {
                Keyframe key = new Keyframe(time, value);
                keys.Add(key);
            }
        }

        Animation.ClearCurves();

        AnimationCurve curve = new AnimationCurve(keys.ToArray());

        for (int i = 0; i < keys.Count; i++)
        {
            AnimationUtility.SetKeyLeftTangentMode(curve, i, AnimationUtility.TangentMode.ClampedAuto);
            AnimationUtility.SetKeyRightTangentMode(curve, i, AnimationUtility.TangentMode.ClampedAuto);
        }

        Animation.SetCurve("", typeof(RectTransform), PropertyName, curve);
    }
}
