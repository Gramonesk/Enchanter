using UnityEngine;

public class Targetable : MonoBehaviour
{
    [HideInInspector] public Card user;
    private SpriteRenderer display;
    private Collider2D coll;
    public Target_type type;
    private void Awake()
    {
        coll = GetComponent<Collider2D>();
        user = GetComponentInParent<Card>();      
        display = GetComponentInParent<SpriteRenderer>();
    }
    public void Enable(bool con)
    {
        coll.enabled = con;
        display.enabled = con;
    }
    private void OnMouseDown()
    {
        TargetManager.instance.Insert(user);
    }
}
