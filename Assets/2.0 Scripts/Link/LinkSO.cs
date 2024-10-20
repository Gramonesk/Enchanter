using System;
using System.Collections.Generic;
using UnityEngine;
[Flags]
public enum Target_type
{ enemy = 1, allies = 2, self = 4, dead = 8};
public enum Attack_type
{ AOE, single }
public enum Link_type
{ Offensive, Defensive, Support, Action, Debuff };

[CreateAssetMenu(fileName ="Link")]
public class LinkSO : ScriptableObject
{
    [Header("Link Settings")]
    [Tooltip("This LinkType")]
    public Link_type Linker;
    [Tooltip("Supports the LinkType")]
    public Link_type Nexus;

    [Header("Effect Settings\n" +
    "=======================================")]
    public EffectParameter Basic;

    [Space(10)]
    public EffectParameter Skill;
}
[Serializable]
public struct EffectParameter
{
    [SerializeField] AnimationName animationName;
    [SerializeField] float ManaUsed;
    public readonly float _ManaUsed => ManaUsed;
    public readonly string _animationName => Enum.GetName(typeof(AnimationName), animationName);
    [Space(10)]
    [TextArea(3,3)]
    public string EffectDescription;
    [ArrayElementTitle("effect")]
    public List<ActionEffects> effects;
    //public List<Target_type> target_type;
    public Target_type target_type;
    public Attack_type attack_type;
}
