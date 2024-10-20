using System;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
/// <summary>
/// Dipake bwt reference Deck sendiri / deck musuh
/// bwt Database jg bisa (masukinnya manual, isi Character aj)
/// </summary>
[CreateAssetMenu(fileName = "DeckData")]
public class DeckData : ScriptableObject
{
    public List<CardSO> Characters;

    [Header("Only fill for gameplay")]
    public List<LinkSO> Links;
}
[Serializable]
public struct CharacterProfile
{
    public CardSO Character_Data;
    public AnimatorController controller;
    //public List<LinkSO> Links;
}
