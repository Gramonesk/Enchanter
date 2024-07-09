using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckManager : MonoBehaviour, IPointerDownHandler
{
    //TO DO : 
    //View Decklist w/IPointerDownHandler
    //UI untuk deck

    //private DeckDisplay display;
    public List<LinkSO> Deck = new();
    public Action<LinkSO> AddToHand;
    private int max;
    public bool TryDrawCard()
    {
        if (max <= 0) return false;
        AddToHand(Deck[max - 1]);
        return true;
    }
    public void GetDeck(List<LinkSO> Decklist)
    {
        Deck = new(Decklist);
        max = Deck.Count;
        Shuffle();
    }
    public void Add(LinkSO item)
    {
        Deck.Add(item);
        max++;
    }
    public void Remove()
    {
        if (max > 0) Deck.Remove(Deck[--max]);
    }
    public void Remove(LinkSO item)
    {
        Deck.Remove(item);
    }
    public void Clear()
    {
        Deck.Clear();
    }
    public void Shuffle()
    {
        LinkSO temp;
        for (int i = 0; i < max; i++)
        {
            int rand = UnityEngine.Random.Range(i, max);

            temp = Deck[i];
            Deck[i] = Deck[rand];
            Deck[rand] = temp;
        }
    }
    public void OnPointerDown(PointerEventData eventdata)
    {
        //show uinya
        //parameter : List Deck + max
    }
}
