public class HealEffect : Effect
{
    public HealEffect(ActionEffects data) : base(data)
    {
    }
    public override void Activate(Card target, Card user)
    {
        Card ActualTarget = data.targetting_type.Equals(Target_type.enemy) ? target : user;
        ActualTarget.Health += data.Value(target, user);
    }
}
public class Block : Effect
{
    public Block(ActionEffects data) : base(data)
    {
    }
    public override void Activate(Card target, Card user)
    {
        Card ActualTarget = data.targetting_type.Equals(Target_type.enemy) ? target : user;
        ActualTarget.Block += data.Value(target, user);
    }
}
public class Inflict_Buff : Effect
{
    public Inflict_Buff(ActionEffects data) : base(data)
    {
    }
    public override void Activate(Card target, Card user)
    {
        Card ActualTarget = data.targetting_type.Equals(Target_type.enemy) ? target : user;
        foreach (var info in data.Linger_Effects) ActualTarget.AddBuff(info);
    }
}
public class ColorChange : Effect
{
    public ColorChange(ActionEffects data) : base(data)
    {
    }
    public override void Activate(Card target, Card user)
    {
        throw new System.NotImplementedException();
    }
}
public class Draw : Effect
{
    public Draw(ActionEffects data) : base(data)
    {
    }
    public override void Activate(Card target, Card user)
    {
        throw new System.NotImplementedException();
    }
}
public class InflictDamage : Effect
{
    public InflictDamage(ActionEffects data) : base(data)
    {
    }
    public override void Activate(Card target, Card user)
    {
        Card ActualTarget = data.targetting_type.Equals(Target_type.enemy) ? target : user;
        ActualTarget.ReceiveDamage(data.Value(target, user));
    }
}
public abstract class Effect
{
    protected ActionEffects data;
    public Effect(ActionEffects data)
    {
        this.data = data;
    }
    public abstract void Activate(Card target, Card user);
}
