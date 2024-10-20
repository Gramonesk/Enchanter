using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New", menuName = "Card", order = 1)]
public class CardSO : ScriptableObject
{
    public string Name;
    public Sprite sprite;
    //keep links so that they can be generated upon starting a battle
    public List<LinkSO> links;

    public enum char_category { Ace, Guard, Fighter, Disruptor, Support, Linker, Playmaker }
    public char_category Character_Category;
    public enum leader_Type { Instinctive, Strategic, None };
    public leader_Type Leader_Type;
    #region stats
    public int hp_max = 100;
    public int mana_max = 100;
    public int atk, magatk, def, dex;

    [ArrayElementTitle("animationName")]
    public List<AnimationInformation> anim_info;
    #endregion 
}