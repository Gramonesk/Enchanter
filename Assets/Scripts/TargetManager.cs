using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
public class TargetManager : MonoBehaviour
{
    public static TargetManager instance;
    private List<Targetable> Object;
    private Dictionary<Target_type, List<Targetable>> Objects = new();

    List<Target_type> Targetting;
    Attack_type Attacking;

    private readonly List<Card> targets = new();
    private void Awake()
    {
        instance = this;
    }
    public void Set()
    {
        Object = FindObjectsOfType<Targetable>().ToList();
        Objects.Add(Target_type.enemy, new());
        Objects.Add(Target_type.allies, new());
        Objects.Add(Target_type.self, new());
        foreach (var obj in Object) Objects[obj.type].Add(obj);
    }
    public void Insert(Card card)
    {
        targets.Add(card);
    }
    public IEnumerator StartTarget(LinkSO data, bool isAbility)
    {
        targets.Clear();
        Targetting = isAbility ? data.skill_target : data.basic_target;
        Attacking = isAbility ? data.skill_attType : data.basic_attType;
        if (Targetting.Contains(Target_type.self))
        {
            Objects[Target_type.self] = new()
            {
                data.user.GetComponentInChildren<Targetable>()
            };
        }
        foreach (var type in Targetting)
        {
            foreach (var obj in Objects[type]) obj.Enable(true);
        }
        while (targets.Count <= 0) yield return null;
        foreach (var obj in Object) obj.Enable(false);
    }
    public List<Card> GetTargets()
    {
        if (Attacking == Attack_type.AOE)
        {
            foreach (var target in targets.ToList())
            {
                var type = target.GetComponentInChildren<Targetable>().type;
                foreach (var obj in Objects[type]) Insert(obj.user);
            }
        }
        return targets;
    }
    public static List<T> FindInterfacesOfType<T>()
    {
        IEnumerable<T> objects = FindObjectsOfType<MonoBehaviour>(true).OfType<T>();
        return new List<T>(objects);
    }
}
