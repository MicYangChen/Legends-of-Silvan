using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelConfig : ScriptableObject
{
    [Header("Animation Curve")]
    public AnimationCurve animationCurve;
    public int MaxLevel;
    public int MaxRequiredExp;

    public int GetRequiredExp(int level)
    {
        int requiredExp = Mathf.RoundToInt(animationCurve.Evaluate(Mathf.InverseLerp(0, MaxLevel, level)) * MaxRequiredExp);
        return requiredExp;
    }
}
