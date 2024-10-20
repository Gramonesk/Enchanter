using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;
    private readonly Dictionary<Target_type, List<Targetable>> Objects = new();
    public List<Card> Target
    {
        get
        {
            if (_Effect.attack_type == Attack_type.AOE)
            {
                var type = targets[0].GetComponentInChildren<Targetable>().type;
                targets.RemoveAt(0);
                foreach (var obj in Objects[type]) Insert(obj.user);
            }
            return targets;
        }
    }

    EffectParameter _Effect;
    private readonly List<Card> targets = new();
    List<Targetable> targetList = new();
    public int Count(Target_type type) => Objects[type].Count;
    public void Insert(Card card) => targets.Add(card);
    private void Awake()
    {
        instance = this;
        foreach (Target_type type in Enum.GetValues(typeof(Target_type))) Objects.Add(type, new());
    }
    public void RefreshTargets()
    {
        var Object = FindObjectsOfType<Targetable>().ToList();
        foreach (Target_type type in Enum.GetValues(typeof(Target_type))) Objects[type].Clear();
        foreach (var obj in Object) Objects[obj.type].Add(obj);
    }
    public IEnumerator StartTarget(Link data, bool isAbility)
    {
        SetTargetInfo(data, isAbility);
        ShowTargets(true);
        while (targets.Count <= 0) yield return null;
        Debug.Log("fin");
        //yield return new WaitUntil(() => targets.Count > 0);
        ShowTargets(false);
    }
    public void SetTargetInfo(Link data, bool isAbility)
    {
        targets.Clear();
        targetList.Clear();
        _Effect = data.GetAttack(isAbility);
        if (_Effect.target_type.HasFlag(Target_type.self))
        {
            Objects[Target_type.self] = new(){
                data.user.GetComponentInChildren<Targetable>()
            };
        }
        GetTargetList();
    }
    public List<Target_type> ConvertFlagToList(Target_type data)
    {
        List<Target_type> listedEnum = new();
        foreach(Target_type type in Enum.GetValues(typeof(Target_type)))
        {
            if (data.HasFlag(type)) listedEnum.Add(type);
        }
        return listedEnum;
    }
    public void GetTargetList()
    {
        var EffectList = ConvertFlagToList(_Effect.target_type);
        foreach (var type in EffectList)
        {
            foreach (var obj in Objects[type]) targetList.Add(obj);
        }
    }
    public void ShowTargets(bool condition)
    {
        foreach (var target in targetList) target.Enable(condition);
    }
    #region ENEMY_TARGETTING HANDLER
    public void EnemyGetTargets()
    {
        int targetIndex = UnityEngine.Random.Range(0, targetList.Count);
        Insert(targetList[targetIndex].user);
    }
    public void GetPointsToDetermineOptimalTarget()
    {
        //var Effects = Effect.effects;
        //(int, Effect_type) max = (0, Effect_type.InflictDamage);
        //List<Effect_type> types = new();
        //foreach (var effect in Effects) types.Add(effect.effecttype);
        //Dictionary<Effect_type, int> PointBehaviour = new();
        //foreach(var type in types)
        //{
        //    int value = 0;
        //    switch (type)
        //    {
        //        case Effect_type.InflictDamage:
        //            break;
        //        case Effect_type.Block:
        //            break;
        //        case Effect_type.Inflict_Buff:
        //            break;
        //        case Effect_type.Draw:
        //            break;
        //        case Effect_type.Heal:
        //            break;
        //        case Effect_type.ColorChange:
        //            break;
        //    }
        //    if (value > max.Item1) max = (value, type);
        //}
    }
    #endregion
}
