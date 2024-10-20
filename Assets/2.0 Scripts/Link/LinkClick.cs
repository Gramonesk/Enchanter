using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(LinkDisplay))]
public class LinkBase : MonoBehaviour
{
    public LinkDisplay display { get; private set; }

    public bool isActive { get => display._isActive; }
    public virtual void Awake()
    {
        display = GetComponent<LinkDisplay>();
    }
}

public class LinkClick : LinkBase, IPointerDownHandler
{
    public Link link;
    public Link Link
    {
        get { return link; }
        set { link = value; display.UpdateLink(link); }
    }
    public Action<LinkClick> OnClicked;
    public void OnPointerDown(PointerEventData cursor) => OnClick();
    public void OnMouseDown() => OnClick();
    public void OnClick()
    {
        if (isActive) OnClicked?.Invoke(this);
    }
}
