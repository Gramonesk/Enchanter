using System.Collections.Generic;
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
