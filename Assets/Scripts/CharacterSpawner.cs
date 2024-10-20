using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public static CharacterSpawner instance;
    [Header("Prefab Configurations")]
    [SerializeField] private GameObject CharacterPrefab;
    [SerializeField] private GameObject HUD_Prefab;

    [Header("Spawn Settings")]
    [SerializeField] private Transform canvas;
    [SerializeField] private Vector2 Offset;
    [Tooltip("The Max Character in a row, when reaching maximum. Offsets by (Y) value of offset")]
    [SerializeField] private float maxCount;

    [Header("Spawn Settings (Player Settings)")]
    [SerializeField] private Transform playerspawn;
    [SerializeField] private Transform playerHUD_panel;

    [Header("Spawn Settings (Enemy Settings)")]
    [SerializeField] private Transform enemyspawn;
    [SerializeField] private Transform enemyHUD_panel;

    private Transform spawn, panel;
    private Vector2 offset;
    private void Awake()
    {
        instance = this;
    }
    public void SpawnCharacters(Deck_list data, Target_type type, Deck deck)
    {
        GetValue(type);
        var characterProfile = data.Characters;
        
        foreach (var character in characterProfile)
        {
            int count = TargetManager.instance.Count(type);
            GameObject Entity = Instantiate(CharacterPrefab, spawn.position + new Vector3(offset.x * (count % maxCount), offset.y * (int)(count / maxCount), 0), Quaternion.identity);
            Card card = Entity.GetComponentInChildren<Card>();
            Entity.GetComponentInChildren<Animator>().runtimeAnimatorController = character.controller;
            
            #region CreateHUD
            var HUD_obj = Instantiate(HUD_Prefab, canvas);
            var HUD = HUD_obj.GetComponent<CharacterHUD>();
            HUD.profil.sprite = character.Character_Data.sprite;

            HUD_obj.transform.SetParent(panel);
            #endregion

            //if (type == Target_type.enemy) Entity.transform.localScale = new Vector3(-1, 1,1);
            card.Set(character.Character_Data, HUD);
            card.GetComponentInChildren<Targetable>().type = type;

            foreach (var linkdata in character.Character_Data.links) //character.links
            {
                Link link = new(linkdata)
                {
                    user = card
                };
                deck.Add(link);
            }
            TargetManager.instance.RefreshTargets();
        }
    }
    private void GetValue(Target_type type)
    {
        if (type == Target_type.allies)
        {
            spawn = playerspawn;
            panel = playerHUD_panel;
            offset = Offset;
        }
        else
        {
            spawn = enemyspawn;
            panel = enemyHUD_panel;
            offset = new Vector2(-Offset.x, Offset.y);
        }
    }
}
//public class CharacterHandler : MonoBehaviour
//{
//    [Header("Main Settings")]
//    [SerializeField] private GameObject CharacterPrefab;
    
//    [SerializeField] private Transform spawn;
//    [SerializeField] private Vector2 offset;
//    [SerializeField] private float maxCount;
//    [SerializeField] private Target_type type;

//    //ntar ada kemungkinan dipake bwt search spawner dengan tipe yg bener
//    public Target_type Type { get => type; }
//    private float count = 0;

//    [Header("UI Settings")]
//    public GameObject HUD_Prefab;
//    public Transform HUD_Panel;
//    public void SpawnCharacter(List<CharacterProfile> data)
//    {
//        foreach(CharacterProfile character in data)
//        {
//            GameObject Entity = Instantiate(CharacterPrefab, spawn.position + new Vector3(offset.x * (count % maxCount), offset.y * ((int)count / maxCount), 0), Quaternion.identity);
//            Card card = Entity.GetComponent<Card>();

//            #region CreateHUD
//            var HUD_obj = Instantiate(HUD_Prefab);
//            var HUD = HUD_obj.GetComponent<CharacterHUD>();

//            HUD.profil.sprite = character.Character_Data.sprite;

//            HUD_obj.transform.SetParent(HUD_Panel);
//            #endregion

//            card.Set(character.Character_Data, HUD);
//            card.GetComponentInChildren<Targetable>().type = type;

//            foreach (var link in character.Links) link.user = card;
//        }
//        TargetManager.instance.SetTargets();
//        count = TargetManager.instance.GetCount(type);
//    }
//}
