using System;
public class Boost : Lingers
{
    public Boost(Card target, Lingering_Effect data) : base(target, data)
    {
    }

    public override void ContinuousEffect()
    {
        throw new NotImplementedException();
    }

    public override void FinishEffect()
    {
        throw new NotImplementedException();
    }

    public override void StartEffect()
    {
        throw new NotImplementedException();
    }
}
public class Poison : Lingers
{
    public Poison(Card target, Lingering_Effect data) : base(target, data)
    {
    }

    public override void ContinuousEffect()
    {
        throw new NotImplementedException();
    }

    public override void FinishEffect()
    {
        throw new NotImplementedException();
    }

    public override void StartEffect()
    {
        throw new NotImplementedException();
    }
}
public class Burn : Lingers
{
    public Burn(Card target, Lingering_Effect data) : base(target, data)
    {
    }

    public override void ContinuousEffect()
    {
        throw new NotImplementedException();
    }

    public override void FinishEffect()
    {
        throw new NotImplementedException();
    }

    public override void StartEffect()
    {
        throw new NotImplementedException();
    }
}
public class Bleed : Lingers
{
    public Bleed(Card target, Lingering_Effect data) : base(target, data)
    {
    }

    public override void ContinuousEffect()
    {
        throw new NotImplementedException();
    }

    public override void FinishEffect()
    {
        throw new NotImplementedException();
    }

    public override void StartEffect()
    {
        throw new NotImplementedException();
    }
}
public class Freeze : Lingers
{
    public Freeze(Card target, Lingering_Effect data) : base(target, data)
    {
    }

    public override void ContinuousEffect()
    {
        throw new NotImplementedException();
    }

    public override void FinishEffect()
    {
        throw new NotImplementedException();
    }

    public override void StartEffect()
    {
        throw new NotImplementedException();
    }
}
public class Stun : Lingers
{
    public Stun(Card target, Lingering_Effect data) : base(target, data)
    {
    }

    public override void ContinuousEffect()
    {
        throw new NotImplementedException();
    }

    public override void FinishEffect()
    {
        throw new NotImplementedException();
    }

    public override void StartEffect()
    {
        throw new NotImplementedException();
    }
}
public class Heal : Lingers
{
    public Heal(Card target, Lingering_Effect data) : base(target, data)
    {
    }

    public override void ContinuousEffect()
    {
        throw new NotImplementedException();
    }

    public override void FinishEffect()
    {
        throw new NotImplementedException();
    }

    public override void StartEffect()
    {
        throw new NotImplementedException();
    }
}
public abstract class Lingers
{
    protected Card target;
    protected Lingering_Effect data;
    public Lingers(Card target, Lingering_Effect data)
    {
        this.target = target;
        this.data = data;
    }
    public abstract void StartEffect();
    public abstract void ContinuousEffect();
    public abstract void FinishEffect();
}