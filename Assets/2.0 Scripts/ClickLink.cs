using System;
using UnityEngine;

[RequireComponent(typeof(Link), typeof(LinkDisplay), typeof(Clickable))]
public class ClickLink : MonoBehaviour
{
    public Link link;
    private LinkDisplay display;

    public bool isActive { get => display._isActive; }
    private Clickable click;


    public Action<ClickLink> OnClicked;
    private void Start()
    {
        //link = GetComponent<Link>();
        display = GetComponent<LinkDisplay>();
        click = GetComponent < Clickable>();
        click.OnClicked = OnClick;
    }
    private void OnClick()
    {
        OnClicked.Invoke(this);
    }
    public void Clear()
    {
        display.Clear();
    }
}
