using System;
using System.Collections.Generic;
using UnityEngine;
public class Deck : MonoBehaviour
{
    // jgn pake scriptableobject, susah bwt saving between game
    private List<Link> deck;
    public List<Link> DeckInfo {get => deck;}

    //Remove later, still for debug and optimization
    public Deck()
    {
        deck = new();
    }
    public Deck(List<Link> DeckList)
    {
        GetDeck(DeckList);
    }
    //Remove later, still for debug and optimization

    private int Count { get => deck.Count; }
    /// <summary>
    /// Will try to draw a card, returns true if it succeeded
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool DrawCard(out Link data)
    {
        data = null;
        if (Count <= 0) return false;
        data = deck[Count - 1];
        return true;
    }
    public void GetDeck(List<Link> Decklist)
    {
        deck = new(Decklist);
        Shuffle();
    }
    public void Add(Link item)
    {
        deck.Add(item);
    }
    public void Remove()
    {
        if (Count > 0) deck.RemoveAt(Count-1);
    }
    public void Remove(Link item)
    {
        deck.Remove(item);
    }
    public void Clear()
    {
        deck.Clear();
    }
    public void Shuffle()
    {
        for (int i = 0; i < Count; i++)
        {
            int rand = UnityEngine.Random.Range(i, Count);
            (deck[rand], deck[i]) = (deck[i], deck[rand]);
        }
    }
}