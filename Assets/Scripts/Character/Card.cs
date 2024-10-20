using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Card_params
{
    health,mana,atk,mag,def,dex, block
}
[Serializable]
public struct AnimationInformation
{
    [SerializeField] private AnimationName animationName;
    public readonly string _animationName => Enum.GetName(typeof(AnimationName), animationName);
    [Tooltip("Dipake buat frame dimana dia kena hit dll")]
    [SerializeField] int EffectTriggerFrameIndicator;
    [SerializeField] int AnimationTotalFrames;
    public readonly float normalizedTime => (float)EffectTriggerFrameIndicator / AnimationTotalFrames;
}
public enum DMG_Text
{
    ReceiveDMG, Heal, ReceiveDMGOnBlock, BlockUP
}
[Serializable]
public struct TextVariant
{
    public DMG_Text type;
    public Color color;
}
public class Card : MonoBehaviour
{
    public Animator animator;
    public CharacterHUD HUD;
    public List<Lingering_Effect> Buffs = new();
    [HideInInspector] public List<LinkSO> links;
    private List<AnimationInformation> anim_info = new();
    #region PlayerStats
    private float MaxHealth;
    private float MaxMana;
    private float health;
    public float Health
    {
        get => health;
        set
        {
            health = Mathf.Clamp(value, 0, MaxHealth);
            DamageTextManager.instance.ShowDamageText(value, value < 0 ? DMG_Text.ReceiveDMG : DMG_Text.Heal, transform);
            HUD.HP = health;
        }
    }
    private float mana;
    public float Mana
    {
        get => mana;
        set
        {
            mana = Mathf.Clamp(value, 0, MaxMana);
            HUD.MP = mana;
        }
    }
    private float block;
    public float Block
    {
        get => block;
        set
        {
            if(block < 0)
            {
                Health += block;
                block = 0;
            }
            else
            {
                DamageTextManager.instance.ShowDamageText(value, value < 0 ? DMG_Text.ReceiveDMGOnBlock : DMG_Text.BlockUP, transform);
            }
        }
    }
    public float Physical_Attack;
    public float Magic_Attack;
    public float Defense;
    public float Dexterity;

    public float Damage_Reduc;
    #endregion
    public void Set(CardSO data, CharacterHUD hud)
    {
        GetComponent<SpriteRenderer>().sprite = data.sprite;
        HUD = hud;
        
        links = data.links;
        anim_info = data.anim_info;

        MaxHealth = data.hp_max;
        HUD.MaxHP = MaxHealth;
        Health = MaxHealth;

        MaxMana = data.mana_max;
        HUD.MaxMP = MaxMana;
        Mana = MaxMana;

        Physical_Attack = data.atk;
        Magic_Attack = data.magatk;
        
        Defense = data.def;
        Dexterity = data.dex;
    }
    public void AddBuff(Lingering_Effect buff)
    {
        if (buff._isStackable)
        {
            buff.ExpirationTime = TurnSystem.turn + buff.EffectDuration;
            buff.Apply(Apply_type.Start);
            buff.Init(this);
            Buffs.Add(buff);
        }
        else
        {
            Buffs.Find(x => x == buff).ExpirationTime = TurnSystem.turn + buff.EffectDuration;
            //Buffs.Find(x => x == buff).OnStack();
        }
    }
    //invoke to TurnSystem
    public void OnNewTurn(float Turn)
    {
        foreach (var buff in Buffs)
        {
            if (Turn >= buff.ExpirationTime)
            {
                buff.Apply(Apply_type.Finish);
                Buffs.Remove(buff);
            }
            else buff.Apply(Apply_type.Continuous);
        }
    }
    public IEnumerator PlayAnimation(string animation_name, Action toDo)
    {
        var info = anim_info.Find(x => animation_name == x._animationName);
        yield return AnimationUtils.PlayAndWaitForAnim(animator, animation_name, toDo, info.normalizedTime);
    }
    public void ReceiveDamage(float DamageValue)
    {
        if (Defense / 2 > DamageValue) DamageValue = 0;
        DamageValue -= (DamageValue * Defense / Defense + 100);
        animator.Play("Damaged");
        block -= DamageValue;
    }
}
public static class AnimationUtils
{
    public static IEnumerator PlayAndWaitForAnim(Animator targetAnim, string stateName)
    {
        targetAnim.Play(stateName);
        yield return new WaitUntil(() => targetAnim.GetCurrentAnimatorStateInfo(0).IsName(stateName));
        yield return new WaitUntil(() => targetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
    }
    public static IEnumerator PlayAndWaitForAnim(Animator targetAnim, string stateName, Action action, float normalized_time)
    {
        targetAnim.Play(stateName);
        yield return new WaitUntil(() => targetAnim.GetCurrentAnimatorStateInfo(0).IsName(stateName));
        yield return new WaitUntil(() => targetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= normalized_time);
        action?.Invoke();
        yield return new WaitUntil(() => targetAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
    }
}