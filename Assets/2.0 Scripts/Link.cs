using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Link
{
    public Card user;
    #region Variables
    //Knp begini?
    //1. Modification (bisa dimodify via in game tanpa ngerusak yang lain)
    //2. Link Appliable for numerous usage (bisa dipake banyak kartu dan dmn mn)
    public Link_type Linker { get; private set; }
    public Link_type Nexus { get; private set; }
    public EffectParameter Basic { get; private set; }
    public EffectParameter Skill { get; private set; }
    public EffectParameter GetAttack(bool isSkill) => !isSkill ? Basic : Skill;
    #endregion
    //Simpen original data for reverting
 
    //buat dpt count tinggal panggil targetmanager
    public LinkSO Origin { get; private set; }
    public Animator clip;
    public Link(LinkSO data)
    {
        Set(data);
    }
    public void Set(LinkSO data)
    {
        Origin = data;
        //set links
        Linker = data.Linker;
        Nexus = data.Nexus;

        Basic = data.Basic;
        Skill = data.Skill;
    }
    public bool CheckCondition(Link previous_link)
    {
        return previous_link.Nexus == Linker && Skill._ManaUsed <= user.Mana;
    }
    public IEnumerator ActivateCTN(List<Card> targets, bool isAbility)
    {
        EffectParameter eff = isAbility ? Skill : Basic;
        yield return user.PlayAnimation(eff._animationName, () =>
        {
            foreach (Card card in targets)
            {
                var Effects = eff.effects;
                foreach (ActionEffects effect in Effects) effect.Activate(card, user);
            }
        }
        );
    }
}