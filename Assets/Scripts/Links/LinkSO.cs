using System.Collections.Generic;
using UnityEngine;
public enum Target_type
{ enemy, allies, self };
public enum Attack_type
{ AOE, single }
public enum Link_type
{ Offensive, Defensive, Support, Action, Debuff };

[CreateAssetMenu(fileName ="Link")]
public class LinkSO : ScriptableObject
{
    public Card user;
    public List<Effect> Basic;
    public List<Target_type> basic_target;
    public Attack_type basic_attType;

    public List<Effect> Skill;
    public List<Target_type> skill_target;
    public Attack_type skill_attType;

    public Link_type Nexus;
    public Link_type Linker;
}
