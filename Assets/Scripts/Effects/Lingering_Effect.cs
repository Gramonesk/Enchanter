using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EffectHandler
{
    public static void AddValueToTarget(Card_params parameter, Card Target, float value)
    {
        switch(parameter)
        {
            case Card_params.health:
                Target.ReceiveDamage(value);
                break;
            case Card_params.mana:
                Target.Mana += value;
                break;
            case Card_params.atk:
                Target.Physical_Attack += value;
                break;
            case Card_params.mag:
                Target.Magic_Attack += value;
                break;
            case Card_params.def:
                Target.Defense += value;
                break;
            case Card_params.dex:
                Target.Dexterity += value;
                break;
            case Card_params.block:
                Target.Block += value;
                break;
            default:
                break;
        }
    }
}
public enum Apply_type
{
    Start, Continuous, Finish
}
[Serializable]
public class Lingering_Effect
{
    public enum Effect_type
    {
        Heal, Boost, Poison, Burn, Bleed, Freeze, Stun
    }
    [SerializeField] Effect_type effect;

    [Header("Effect Settings")]
    [Tooltip("How long should the effect stay?")]
    public int EffectDuration;

    public float Ammount;
    public bool usePercentage;

    private Lingers LingerEffect;
    [Tooltip("Is the effect repeatable or will it override")]
    [SerializeField] bool isStackable;

    [HideInInspector] public float ExpirationTime;
    public bool _isStackable { get => isStackable; }
    public void Init(Card target) => LingerEffect = GetLingers(target);
    public void Apply(Apply_type type)
    {
        if (type.Equals(Apply_type.Start)) LingerEffect.StartEffect();
        else if(type.Equals(Apply_type.Continuous)) LingerEffect.ContinuousEffect();
        else LingerEffect.FinishEffect();
    }
    Lingers GetLingers(Card target)
    {
        return effect switch
        {
            Effect_type.Heal => new Heal(target, this),
            Effect_type.Boost => new Boost(target, this),
            Effect_type.Poison => new Poison(target, this),
            Effect_type.Burn => new Burn(target, this),
            Effect_type.Bleed => new Bleed(target, this),
            Effect_type.Freeze => new Freeze(target, this),
            Effect_type.Stun => new Stun(target, this),
            _ => throw new NotImplementedException(),
        };
    }
}
