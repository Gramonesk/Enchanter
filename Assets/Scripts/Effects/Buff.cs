using UnityEngine;
public class EffectHandler
{
    /// <summary>
    /// buat ngubah / manipulate stats dari target
    /// </summary>
    /// <param name="parameter"></param>
    public static void SetValue(Card_params parameter, Card Target, float value)
    {
        switch(parameter)
        {
            case Card_params.health:
                Target.InflictDamage(value);
                break;
            case Card_params.mana:
                Target.Mana -= value;
                break;
            case Card_params.atk:
                Target.Physical_Attack -= value;
                break;
            case Card_params.mag:
                Target.Magic_Attack -= value;
                break;
            case Card_params.def:
                Target.Defense -= value;
                break;
            case Card_params.dex:
                Target.Dexterity -= value;
                break;
            default:
                break;
        }
    }
}
public abstract class Effect : ScriptableObject
{
    public abstract void Activate(Card user);
}
public class Buff : Effect
{
    public int Ammount;
    public int EffectDuration;
    public bool IsStackable;
    [HideInInspector] public int TurnElapsed = 0;
    public override void Activate(Card target)
    {
        //Buff new_buff = BuffHandler.New<Buff>(SetData);
        Buff new_buff = CreateInstance<Buff>();
        new_buff.Ammount = Ammount;
        new_buff.EffectDuration = EffectDuration;
        Check(target, new_buff);
    }
    public void Check<T>(Card target, T new_buff) where T : Buff
    {
        var obj = target.Buffs;
        if (IsStackable || !obj.Contains(new_buff)) obj.Add(new_buff);
        else obj[obj.IndexOf(new_buff)] = new_buff;
    }
    public void DoBuff(Card target)
    {
        if (TurnElapsed <= 0) return;
        TurnElapsed--;
        //do something
    }
}
