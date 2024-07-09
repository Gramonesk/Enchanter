using UnityEngine;

[CreateAssetMenu(fileName = "Reduce")]
public class Inflict : Effect
{
    public Card_params target_parameter;
    public float value;
    public override void Activate(Card user)
    {
        Debug.Log("Attacking");
        EffectHandler.SetValue(target_parameter, user, value);
    }
}
