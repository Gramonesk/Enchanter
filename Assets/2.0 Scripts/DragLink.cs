using System;
using UnityEngine;

[RequireComponent(typeof(Link), typeof(LinkDisplay), typeof(Draggable))]
public class DragLink : MonoBehaviour
{
    public Link link;
    private LinkDisplay display;
    public bool isActive { get => display._isActive; }

    private Draggable drag;

    public Action<Link> OnDragged;
    private void Start()
    {
        link = GetComponent<Link>();
        display = GetComponent<LinkDisplay>();
        drag = GetComponent<Draggable>();
        drag.OnDragged = OnEndDrag;
    }
    public void OnEndDrag()
    {
        if (drag.initial_position.y + drag.limitY == drag.transform.position.y)
        {
            drag.IsLocked = true;
            display.SetOFF();
            OnDragged?.Invoke(link);
        }
        drag.transform.position = drag.initial_position;
    }
    public void SetON()
    {
        display.SetON();
        drag.IsLocked = false;
    }
    public void SetOFF()
    {
        display.SetOFF();
        drag.IsLocked = true;
    }
}
