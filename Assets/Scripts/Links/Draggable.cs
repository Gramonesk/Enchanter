using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public interface ILockable
{
    public bool IsLocked { get; set; }
}
public class Draggable : MonoBehaviour, IDragHandler, IEndDragHandler
{
    //ntar dipake bwt register di linkmanager terus getcomponent linkdisplaynya terus kode kek biasa bwt addd
    [Header("Drag Settings")]
    public bool freelook;
    public float limitX;
    public float limitY;

    [HideInInspector] public float xaxis, yaxis;
    [HideInInspector] public Vector3 initial_position;

    public bool IsLocked { get => islocked; set => islocked = value; }
    public bool islocked;
    public Action OnDragged { get; set; }

    public void Awake()
    {
        initial_position = transform.position;
    }
    public void OnEndDrag(PointerEventData mouse)
    {
        if (islocked) return;
        OnDragged?.Invoke();
    }
    public void OnDrag(PointerEventData mouse)
    {
        if (islocked) return;
        //float YMouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        //float XMouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        float YMouseInput = mouse.position.y;
        float XMouseInput = mouse.position.x;
        if (freelook)
        {
            yaxis = Mathf.Clamp(YMouseInput, initial_position.y, initial_position.y + limitX);
            xaxis = Mathf.Clamp(XMouseInput, initial_position.x, initial_position.x + limitY);
        }
        transform.position = new Vector2(xaxis, yaxis);
    }

}
