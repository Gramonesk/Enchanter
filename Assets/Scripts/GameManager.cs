using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FileData;
using System.ComponentModel;
public class GameManager : MonoBehaviour
{
    //pake save folder ngambil data dari ini
    //yg disimpen di folder untuk sementara cmn index dr database sih
    //Char_index (bwt character), Index link yg digunakan (0, 2, 3, 4) <- Contoh
    //fetching data sm skillnya masih dipertanyakan (mungkin pake enum buat support, tp masukin dict jg jadinya (agak ngebruteforce)

    //Database yg di fetch / reference itu Database yg bakal dipake di inventory preset
    //Party settings -> choose this config(inv.preset save semua yg lagi dipake situ)

    [Header("Game Settings")]
    public DeckData PlayerDeck_dataREF;
    public DeckData EnemyDeck_dataREF;

    public LinkManager PlayerManager;
    public LinkManager EnemyManager;

    [Header("Creation Settings")]
    public List<Transform> PlayerCharacter_positions;
    public List<Transform> EnemyCharacter_positions;
    public GameObject Character_Prefab;
    public GameObject HUD_Prefab;
    public GameObject HUD_Panel;

    private List<IDragLink> DragLinks;
    //NTS : Statemachine ntaran, yg penting fungsional dlu, dibrute force gpp ntar libur rombak ulang
    //      gausah mikirin bagus dlu, gw tau bakal lu paksa
    public void StartState()
    {
        PlayerManager.Decklist = PlayerDeck_dataREF.Links;
        EnemyManager.Decklist = EnemyDeck_dataREF.Links;

        //SetupCharacter(PlayerDeck_dataREF, true, PlayerCharacter_positions, Target_type.allies);
        //SetupCharacter(EnemyDeck_dataREF, false, EnemyCharacter_positions, Target_type.enemy);

        PlayerManager.Set();
        EnemyManager.Set();
        PlayerManager.Draw(5);
        EnemyManager.Draw(5);
        TargetManager.instance.Set();
    }
    public void SetupCharacter(DeckData Data, bool AlsoCreateHUD, List<Transform> spawn, Target_type type)
    {
        int x = 0;
        foreach (CardSO card_data in Data.Characters)
        {
            GameObject chara = Instantiate(Character_Prefab, spawn[x++].position, Quaternion.identity);
            Card character = chara.GetComponent<Card>();
            character.Activate(card_data);
            if (AlsoCreateHUD)
            {
                var Ui = Instantiate(HUD_Prefab);
                Ui.transform.SetParent(HUD_Panel.transform);
                var HUD = Ui.GetComponent<CharacterHUD>();
                HUD.MaxHP = card_data.hp_max;
                HUD.MaxMP = card_data.mana_max;
                HUD.profil.sprite = card_data.char_sprite;
            }
            character.GetComponentInChildren<Targetable>().type = type;
            List<LinkSO> link_related = Data.Links.FindAll(x => character.links.Contains(x));
            foreach(var link in link_related)
            {
                link.user = character;
                Debug.Log(card_data.name);
            }
            
        }
    }
    public void PrepState()
    {
        foreach (var link in DragLinks) link.Locked = false;
        //pas target ntar jgn lupa pake input system atau semacemnya biar ga koalisi coroutine
    }
    public void BattleState()
    {
        //Handler.run
    }
    // Start is called before the first frame update
    void Awake()
    {
        DragLinks = FindObjectsOfType<MonoBehaviour>().OfType<IDragLink>().ToList();
        StartState();
    }
}
