using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct DeployData
{
    public LinkSO data;
    public bool IsAbility;
    public List<Card> targets;
};
public class LinkManager : MonoBehaviour
{
    [Header("Decks")]
    [SerializeField] private DeckManager Deck;
    [SerializeField] private DeckManager Grave;

    [Header("Slots")]
    public GameObject Deploy;
    public GameObject Queue;
    private List<LinkDrag> HandSlot = new(5);
    private List<LinkClick> DeploySlot = new(5);

    //[Header("Data")]
    [HideInInspector] public List<LinkSO> Decklist;

    public List<DeployData> DeployQueue = new();
    private List<LinkDisplay> Deployed = new();
    public void Set()
    {
        Deck.GetDeck(Decklist);
        DeploySlot = Queue.GetComponentsInChildren<LinkClick>().ToList();
        HandSlot = Deploy.GetComponentsInChildren<LinkDrag>().ToList();

        Deck.AddToHand = AllocateCard;
        foreach (var hand in HandSlot) hand.OnDeploy = Register;
        foreach (var deploy in DeploySlot) deploy.OnCancel = UnRegister;
    }
    public void Draw(int iteration)
    {
        for (int i = 0; i < iteration; i++)
        {
            if (!Deck.TryDrawCard())
            {
                Deck.GetDeck(Grave.Deck);
                Grave.Clear();
                Deck.TryDrawCard();
            }
            Deck.Remove();
        }
    }
    public IEnumerator Register(LinkSO card, LinkDisplay source)
    {
        int index = DeployQueue.Count;
        yield return Insert(card, index, source);
        DeploySlot[index].UpdateLink(card);
    }
    public void UnRegister(LinkClick source)
    {
        int index = DeploySlot.IndexOf(source);
        int count = DeployQueue.Count;
        for (int i = index; i < count; i++)
        {
            if (DeploySlot[i]._isActive)
            {
                DeployQueue.RemoveAt(index);
                Deployed[index].SetON();
                Deployed.RemoveAt(index);
                DeploySlot[i].Clear();
            }
        }
    }
    public void AllocateCard(LinkSO card)
    {
        int index = HandSlot.FindIndex(x => !x._isActive);
        if (index == -1) Debug.LogError("Failed to Add Card");
        HandSlot[index].UpdateLink(card);
    }
    public IEnumerator Insert(LinkSO card, int index, LinkDisplay disp)
    {
        bool condition = index - 1 >= 0 && DeployQueue[index - 1].data.Nexus == card.Linker;
        yield return TargetManager.instance.StartTarget(card, condition);
        DeployQueue.Add(new DeployData
        {
            data = card,
            IsAbility = condition,
            targets = TargetManager.instance.GetTargets()
        });
        Deployed.Add(disp);
    }
    public void Ready()
    {
        BattleHandler.Player_Deploydata = DeployQueue;
        DeployQueue.Clear();
        Deployed.Clear();
        foreach(var slot in DeploySlot)
        {
            slot.Clear();
        }
    }
}
