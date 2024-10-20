using System.Collections.Generic;
using System;
using UnityEngine;

public enum Effect_type
{
    InflictDamage, Heal, Block, Draw, ColorChange, Inflict_Buff
}
[Serializable]
public class ActionEffects
{
    public enum Value_parameter { user, target }

    [SerializeField] Effect_type effect;
    [Header("Action Settings\n=======================")]
    [SerializeField] float Ammount;
    [SerializeField] bool usePercentage;

    [Header("Using Percentage Settings")]
    [SerializeField] Value_parameter UseParameter;
    [SerializeField] Card_params TypeOfParameter;

    [Header("Buff or damaging purposes (ENEMY / ALLY)")]
    [SerializeField] Target_type Target_Effect;

    [Header("Inflict_Buff Settings")]
    [ArrayElementTitle("effect")]
    [SerializeField] List<Lingering_Effect> LingerEffects;

    public List<Lingering_Effect> Linger_Effects => LingerEffects;
    public Effect_type effecttype => effect;
    public Target_type targetting_type => Target_Effect;
    private Effect _Effect;
    public void Activate(Card target, Card user)
    {
        _Effect ??= GetEffect();
        _Effect.Activate(target, user);
    }
    public Effect GetEffect()
    {
        return effect switch
        {
            Effect_type.InflictDamage => new InflictDamage(this),
            Effect_type.Heal => new HealEffect(this),
            Effect_type.Block => new Block(this),
            Effect_type.Draw => new Draw(this),
            Effect_type.ColorChange => new ColorChange(this),
            Effect_type.Inflict_Buff => new Inflict_Buff(this),
            _ => throw new NotImplementedException()
        };
    }
    public float Value(Card Target, Card User)
    {
        if (usePercentage)
        {
            Card CharacterToUse = UseParameter.Equals(Value_parameter.target) ? Target : User;
            return Ammount/100 * EffectUtils.GetParameterValue(CharacterToUse, TypeOfParameter);
        }
        return Ammount;
    }
}
public static class EffectUtils
{
    public static float GetParameterValue(Card Character, Card_params TypeOfParameter)
    {
        return TypeOfParameter switch
        {
            Card_params.health => Character.Health,
            Card_params.mana => Character.Mana,
            Card_params.atk => Character.Physical_Attack,
            Card_params.mag => Character.Magic_Attack,
            Card_params.def => Character.Defense,
            Card_params.dex => Character.Dexterity,
            Card_params.block => Character.Block,
            _ => throw new NotImplementedException()
        };
    }
}
