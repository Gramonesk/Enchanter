//using System.Collections;
//using System.Collections.Generic;
//using System.Linq;
//using UnityEngine;


//public class LinkManager : MonoBehaviour
//{
//    [Header("Decks")]
//    [SerializeField] private DeckManager Deck;
//    [SerializeField] private DeckManager Grave;

//    [Header("Slots")]
//    public GameObject Deploy;
//    public GameObject Field;
//    private List<LinkDrag> HandSlot = new(5);
//    private List<LinkClick> FieldSlot = new(5);

//    //[Header("Data")]
//    [HideInInspector] public List<LinkSO> Decklist;

//    public List<DeployData> DeployField = new();
//    private List<LinkDisplay> Deployed = new();
//    public void Set()
//    {
//        Deck.GetDeck(Decklist);
//        FieldSlot = Field.GetComponentsInChildren<LinkClick>().ToList();
//        HandSlot = Deploy.GetComponentsInChildren<LinkDrag>().ToList();

//        Deck.AddToHand = AllocateCard;
//        foreach (var hand in HandSlot) hand.OnDeploy = Register;
//        foreach (var deploy in FieldSlot) deploy.OnCancel = UnRegister;
//    }
//    public void Draw(int iteration)
//    {
//        for (int i = 0; i < iteration; i++)
//        {
//            if (!Deck.TryDrawCard())
//            {
//                Deck.GetDeck(Grave.Deck);
//                Grave.Clear();
//                Deck.TryDrawCard();
//            }
//            Deck.Remove();
//        }
//    }
//    public IEnumerator Register(LinkSO card, LinkDisplay source)
//    {
//        int index = DeployField.Count;
//        yield return Insert(card, index, source);
//        FieldSlot[index].UpdateLink(card);
//    }
//    public void UnRegister(LinkClick source)
//    {
//        int index = FieldSlot.IndexOf(source);
//        int count = DeployField.Count;
//        for (int i = index; i < count; i++)
//        {
//            if (FieldSlot[i]._isActive)
//            {
//                DeployField.RemoveAt(index);
//                Deployed[index].SetON();
//                Deployed.RemoveAt(index);
//                FieldSlot[i].Clear();
//            }
//        }
//    }
//    public void AllocateCard(LinkSO card)
//    {
//        int index = HandSlot.FindIndex(x => !x._isActive);
//        if (index == -1) Debug.LogError("Failed to Add Card");
//        HandSlot[index].UpdateLink(card);
//    }
//    public IEnumerator Insert(LinkSO card, int index, LinkDisplay disp)
//    {
//        bool condition = index - 1 >= 0 && DeployField[index - 1].data.Nexus == card.Linker;
//        yield return TargetManager.instance.StartTarget(card, condition);
//        DeployField.Add(new DeployData
//        {
//            data = card,
//            IsAbility = condition,
//            targets = TargetManager.instance.GetTargets()
//        });
//        Deployed.Add(disp);
//    }
//    public void Ready()
//    {
//        BattleHandler.Player_Deploydata = DeployField;
//        DeployField.Clear();
//        Deployed.Clear();
//        foreach(var slot in FieldSlot)
//        {
//            slot.Clear();
//        }
//    }
//}
