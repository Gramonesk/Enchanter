using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Card_params
{
    health,mana,atk,mag,def,dex
}
[RequireComponent(typeof(CharacterHUD))]
public class Card : MonoBehaviour
{
    public Animator animator;
    public List<Buff> Buffs = new();
    [HideInInspector] public List<LinkSO> links;
    #region PlayerStats
    private float MaxHealth;
    private float MaxMana;
    public float health;
    public float Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, MaxHealth);
    }
    public float mana;
    public float Mana
    {
        get => mana;
        set => mana = Mathf.Clamp(value, 0, MaxMana);
    }
    public float block;
    public float Block
    {
        get => block;
        set => block = Mathf.Clamp(value, 0, value);
    }
    public float Physical_Attack;
    public float Magic_Attack;
    public float Defense;
    public float Dexterity;
    public float Accuracy;

    public float Damage_Reduc;
    #endregion
    public void Activate(CardSO data)
    {
        GetComponent<SpriteRenderer>().sprite = data.char_sprite;
        links = data.links;
        MaxHealth = data.hp_max;
        MaxMana = data.mana_max;
        Physical_Attack = data.atk;
        Magic_Attack = data.magatk;
        Defense = data.def;
        Dexterity = data.dex;
        //foreach link set its user to this
    }
    public void OnNewTurn()
    {
        foreach (Buff buff in Buffs)
        {
            if (buff.TurnElapsed <= 0) Buffs.Remove(buff);
            else buff.DoBuff(this);
        }
    }
    public void InflictDamage(float value)
    {
        var BlockValue = value >= 0 ? Block - value : -value;
        if(BlockValue < 0 || value < 0)
        {
            Health += BlockValue;
        }
    }
}
