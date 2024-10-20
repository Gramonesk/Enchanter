using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class LinkDrag : LinkBase, IDragLink, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Link link;
    public Link Link
    {
        get { return link; }
        set { link = value; display.UpdateLink(link); }
    }
    [Header("Drag Settings")]
    public float Xlimit;
    public float Ylimit;

    //Used By LinkManager
    public Action<LinkDrag> OnDragged;
    private float yaxis;
    private Vector2 initial_position;
    private float difference;

    [SerializeField] private bool locked;
    public bool Locked { get => locked; set => locked = value; }
    public override void Awake()
    {
        base.Awake();
        initial_position = transform.position;
    }

    public void OnMouseDown() => OnStartDrag(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
    public void OnBeginDrag(PointerEventData eventData) => OnStartDrag(eventData.position.y);
    public void OnStartDrag(float value)
    {
        if (Locked || !isActive) return;
        difference = value - initial_position.y;
    }
    public void OnMouseDrag() => OnDragging(Camera.main.ScreenToWorldPoint(Input.mousePosition).y - difference - initial_position.y);
    public void OnDrag(PointerEventData cursor) => OnDragging(cursor.position.y);
    public void OnDragging(float value)
    {
        if (Locked || !isActive) return;
        float YMouseInput = value - difference;
        yaxis = Mathf.Clamp(YMouseInput, 0, Ylimit);
        transform.localPosition = new Vector2(0, yaxis);
    }

    public void OnMouseUp() => OnFinishDrag();
    public void OnEndDrag(PointerEventData cursor) => OnFinishDrag();
    public void OnFinishDrag()
    {
        Debug.Log("Finish Dragging");
        if (Locked || !isActive) return;
        if (transform.localPosition.y == Ylimit)
        {
            OnDragged?.Invoke(this);
        }
        transform.localPosition = Vector2.zero;
    }
}
