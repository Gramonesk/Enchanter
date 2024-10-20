using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Deck")]
public class Deck_list : ScriptableObject
{
    public List<CharacterProfile> Characters;
}
