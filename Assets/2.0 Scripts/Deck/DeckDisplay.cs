using UnityEngine;
using UnityEngine.EventSystems;

public class DeckDisplay : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Deck _deck;
    public void OnPointerClick(PointerEventData eventData)
    {
        //_deck.DeckInfo (ambil deck bwt dipake display)
        //Open the display tab and show all possible card
        //put in invoker first!
    }
}