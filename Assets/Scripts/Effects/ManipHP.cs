using UnityEngine;

public class ManipHP : Effect
{
    public float ammount;
    public override void Activate(Card user)
    {
        user.InflictDamage(ammount);
    }
}
//public class BuffHandler
//{
//    public static T New<T>(Func<T, T> SetData) where T : ScriptableObject
//    {
//        T new_buff = ScriptableObject.CreateInstance<T>();
//        new_buff = SetData(new_buff);
//        return new_buff;
//    }
//}
//public Buff SetData(Buff data)
//{
//    data.EffectDuration = EffectDuration;
//    data.Ammount = Ammount;
//    return data;
//}
