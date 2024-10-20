using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;
public struct DeployData
{
    public Link link;
    public bool isAbility;
    public List<Card> targets;
};
public class DeckManager : MonoBehaviour
{
    [Header("Deck Settings")]
    public Deck deck;
    public Deck grave;

    [Header("Slot Settings")]
    [SerializeField] GameObject Hand;
    [SerializeField] GameObject Field;

    private List<LinkDrag> HandSlot = new(5);
    private List<LinkClick> FieldSlot = new(5);

    private Queue<DeployData> Queue = new();
    public List<DeployData> data => Queue.ToList();
    private Queue<LinkDrag> deployed = new();
    Coroutine coroutine;
    public void Awake()
    {
        FieldSlot = Field.GetComponentsInChildren<LinkClick>().ToList();
        HandSlot = Hand.GetComponentsInChildren<LinkDrag>().ToList();

        foreach (var slot in HandSlot) slot.OnDragged = AddToField;
        foreach (var slot in FieldSlot) slot.OnClicked = RemoveFromField;
    }
    public List<Link> GetHandData()
    {
        List<Link> data = new();
        foreach(var slot in HandSlot)
        {
            data.Add(slot.link);
        }
        return data;
    }
    public void Draw(int iteration)
    {
        for (int i = 0; i < iteration; i++)
        {
            if (!deck.DrawCard(out Link data))
            {
                deck.GetDeck(grave.DeckInfo);
                grave.Clear();
                deck.DrawCard(out data);
            }
            if (AllocateLink(data)) deck.Remove();
        }
    }
    /// <summary>
    /// If allocation of data is successful, returns a true
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public bool AllocateLink(Link card)
    {
        int index = HandSlot.FindIndex(x => !x.isActive);
        if (index == -1) return false;
        HandSlot[index].Link = card;
        return true;
    }
    public void RemoveFromField(LinkClick source)
    {
        int index = FieldSlot.IndexOf(source);
        int count = Queue.Count;
        for (int i = index; i < count; i++)
        {
            if (FieldSlot[i].isActive)
            {
                Queue.Dequeue();
                deployed.Dequeue().display.SetON();
                FieldSlot[i].display.Clear();
            }
        }
    }
    public void Clear()
    {
        var ToDiscard = FieldSlot.FindAll((x) => x.isActive);
        foreach (var card in ToDiscard) Discard(card);
        Queue.Clear();
        deployed.Clear();
    }
    public void Discard(LinkClick card)
    {
        card.display.Clear();
        grave.Add(card.link);
        card.link = null;
    }
    public void AddToField(LinkDrag data)
    {
        coroutine ??= StartCoroutine(InsertToQueue(data));
    }

    public IEnumerator InsertToQueue(LinkDrag drag)
    {
        int index = Queue.Count;
        bool condition = index - 1 >= 0 && drag.link.CheckCondition(Queue.Peek().link);
 
        yield return TargetManager.instance.StartTarget(drag.link, condition);
        Queue.Enqueue(new DeployData
        {
            link = drag.link,
            isAbility = condition,
            targets = TargetManager.instance.Target
        });

        deployed.Enqueue(drag);
        FieldSlot[index].Link = drag.link;
        drag.display.SetOFF();
        coroutine = null;
    }
    public void EnqueueData(DeployData data)
    {
        Queue.Enqueue(data);
    }
}