using AdaStates;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

namespace AdaStates
{
    public abstract class BaseState
    {
        public abstract void OnEnter();
        public abstract void OnExit();
        public abstract void OnLogic();
    }
    public class StateMachine<State> where State : Enum
    {
        private BaseState current_state;
        public State state { get; private set; }
        private readonly Dictionary<State, BaseState> states = new();
        public void OnEnter() { current_state.OnEnter(); }
        public void OnLogic() { current_state.OnLogic(); }
        public void OnExit() { current_state.OnExit(); }
        public void SetState(State state) { current_state = states[state]; this.state = state; }
        public void SwitchState(State state)
        {
            OnExit();
            SetState(state);
            OnEnter();
        }
        public void AddState(State state, BaseState baseState)
        {
            states.Add(state, baseState);
        }
        public void AddState(List<(State, BaseState)> states)
        {
            foreach (var state in states)
            {
                this.states.Add(state.Item1, state.Item2);
            }
        }
    }
}
public enum GameState
{
    Start, Preparation, Battle
}
public class GameManager : MonoBehaviour
{
    [Header("Datas")]
    [SerializeField] private Deck_list PlayerDeckInfo;
    [SerializeField] private Deck_list EnemyDeckInfo;

    [Header("Start State")]
    [SerializeField] private DeckManager PlayerManager;
    [SerializeField] private DeckManager EnemyManager;

    [Header("Preparation State")]
    [SerializeField] private float timeInterval;
    [SerializeField] private int InitialLinks;
    //[SerializeField] private LevelLoader Level;
    //Level loader jg pake objectives, konsepnya tar pake dictionary

    [Header("Battle State")]
    [SerializeField] private int LinksPerTurn;
    public int ready = 0;
    public static StateMachine<GameState> SM = new();
    List<LinkDrag> drags = new();
    private void Awake()
    {
        drags = FindObjectsOfType<LinkDrag>().ToList();
        SM.AddState(new List<(GameState, BaseState)>()
        {
            (GameState.Start, new StartGame(PlayerManager, EnemyManager, PlayerDeckInfo, EnemyDeckInfo, InitialLinks, this)),
            (GameState.Preparation, new PrepareGame(timeInterval, drags, this)),
            (GameState.Battle, new BattleGame(PlayerManager, EnemyManager, LinksPerTurn, this))
        });
        SM.SetState(GameState.Start);
        SM.OnEnter();
    }
    public void LockDrag(bool condition)
    {
        foreach(var drag in drags) drag.Locked = condition;
    }
    private void Update()
    {
        SM.OnLogic();
    }
    public void ChangeState(GameState state)
    {
        SM.SwitchState(state);
    }
    public void Ready()
    {
        ready++;
    }
}
public class StartGame : BaseState
{
    Deck_list pdata, edata;
    DeckManager pdeck, edeck;
    int cards_drawn;
    Action<GameState> changestate;
    public StartGame(DeckManager playerdeck, DeckManager enemydeck, Deck_list playerdata, Deck_list enemydata, int initialcards, GameManager manager)
    {
        pdata = playerdata;
        edata = enemydata;

        pdeck = playerdeck;
        edeck = enemydeck;

        cards_drawn = initialcards;
        changestate = manager.ChangeState;
        manager.LockDrag(true);
    }
    public void DeckSetup(Deck_list data, DeckManager manager, Target_type type)
    {
        CharacterSpawner.instance.SpawnCharacters(data, type, manager.deck);
        manager.deck.Shuffle();
        manager.Draw(cards_drawn);
    }
    public override void OnEnter()
    {
        Debug.Log("Start Phase");
        
        DeckSetup(pdata, pdeck, Target_type.allies);
        DeckSetup(edata, edeck, Target_type.enemy);
        changestate(GameState.Preparation);
    }
    public override void OnExit()
    {

    }
    public override void OnLogic()
    {

    }
}
public class PrepareGame : BaseState
{
    float time, timeInterval;
    GameManager manager;
    List<LinkDrag> drags;
    public PrepareGame(float timeInterval, List<LinkDrag> drags, GameManager manager)
    {
        this.timeInterval = timeInterval;
        this.manager = manager;
        this.drags = drags;
    }
    public override void OnEnter()
    {
        Debug.Log("Prep Phase");
        manager.LockDrag(false);
        time = Time.time + timeInterval;
    }

    public override void OnExit()
    {
        manager.LockDrag(true);
    } 

    public override void OnLogic()
    {
        if (time - Time.time < 0 || manager.ready > 0)
        {
            manager.ready = 0;
            manager.ChangeState(GameState.Battle);
        }
    }
}
public class BattleGame : BaseState
{
    DeckManager EnemyDeck, PlayerDeck;
    Action<GameState> changestate;
    int LinksPerTurn;
    public BattleGame(DeckManager pdeck, DeckManager edeck, int LinksPerTurn, GameManager manager)
    {
        EnemyDeck = edeck;
        PlayerDeck = pdeck;
        changestate = manager.ChangeState;
        this.LinksPerTurn = LinksPerTurn;
    }
    public override void OnEnter()
    {
        BattleHandler.Enemy_Deploydata = EnemyDeck.data;
        BattleHandler.Player_Deploydata = PlayerDeck.data;
        EnemyDeck.Clear();
        PlayerDeck.Clear();
        Debug.Log("Battle Phase");
        PlayerDeck.StartCoroutine(Run());
        //BattleHandler.Run();
        //EnemyDeck.StartCoroutine(BattleHandler.RunCoroutine());
        //changestate(GameState.Preparation);
    }
    public IEnumerator Run()
    {
        yield return BattleHandler.RunCoroutine();
        changestate(GameState.Preparation);
    }
    public override void OnExit()
    {
        EnemyDeck.Draw(LinksPerTurn);
        PlayerDeck.Draw(LinksPerTurn);
    }

    public override void OnLogic()
    {
    }
}
//public class GameManager : MonoBehaviour
//{
//    //pake save folder ngambil data dari ini
//    //yg disimpen di folder untuk sementara cmn index dr database sih
//    //Char_index (bwt character), Index link yg digunakan (0, 2, 3, 4) <- Contoh
//    //fetching data sm skillnya masih dipertanyakan (mungkin pake enum buat support, tp masukin dict jg jadinya (agak ngebruteforce)

//    //Database yg di fetch / reference itu Database yg bakal dipake di inventory preset
//    //Party settings -> choose this config(inv.preset save semua yg lagi dipake situ)

//    [Header("Game Settings")]
//    public DeckData PlayerDeck_dataREF;
//    public DeckData EnemyDeck_dataREF;

//    //public LinkManager PlayerManager;
//    //public LinkManager EnemyManager;

//    [Header("Creation Settings")]
//    public List<Transform> PlayerCharacter_positions;
//    public List<Transform> EnemyCharacter_positions;
//    public GameObject Character_Prefab;
//    public GameObject HUD_Prefab;
//    public GameObject HUD_Panel;

//    private List<IDragLink> DragLinks;
//    //NTS : Statemachine ntaran, yg penting fungsional dlu, dibrute force gpp ntar libur rombak ulang
//    //      gausah mikirin bagus dlu, gw tau bakal lu paksa
//    public void StartState()
//    {
//        //PlayerManager.Decklist = PlayerDeck_dataREF.Links;
//        //EnemyManager.Decklist = EnemyDeck_dataREF.Links;

//        //SetupCharacter(PlayerDeck_dataREF, true, PlayerCharacter_positions, Target_type.allies);
//        //SetupCharacter(EnemyDeck_dataREF, false, EnemyCharacter_positions, Target_type.enemy);

//        //PlayerManager.Set();
//        //EnemyManager.Set();
//        //PlayerManager.Draw(5);
//        //EnemyManager.Draw(5);
//        //TargetManager.instance.Set();
//    }
//    public void SetupCharacter(DeckData Data, bool AlsoCreateHUD, List<Transform> spawn, Target_type type)
//    {
//        int x = 0;
//        foreach (CardSO card_data in Data.Characters)
//        {
//            GameObject chara = Instantiate(Character_Prefab, spawn[x++].position, Quaternion.identity);
//            Card character = chara.GetComponent<Card>();
//            character.Set(card_data);
//            if (AlsoCreateHUD)
//            {
//                var Ui = Instantiate(HUD_Prefab);
//                Ui.transform.SetParent(HUD_Panel.transform);
//                var HUD = Ui.GetComponent<CharacterHUD>();
//                HUD.MaxHP = card_data.hp_max;
//                HUD.MaxMP = card_data.mana_max;
//                HUD.profil.sprite = card_data.char_sprite;
//            }
//            character.GetComponentInChildren<Targetable>().type = type;
//            List<LinkSO> link_related = Data.Links.FindAll(x => character.links.Contains(x));
//            foreach(var link in link_related)
//            {
//                link.user = character;
//                Debug.Log(card_data.name);
//            }

//        }
//    }
//    public void PrepState()
//    {
//        foreach (var link in DragLinks) link.Locked = false;
//        //pas target ntar jgn lupa pake input system atau semacemnya biar ga koalisi coroutine
//    }
//    public void BattleState()
//    {
//        //Handler.run
//    }
//    // Start is called before the first frame update
//    void Awake()
//    {
//        DragLinks = FindObjectsOfType<MonoBehaviour>().OfType<IDragLink>().ToList();
//        StartState();
//    }
//}
