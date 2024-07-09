using System;
using System.Collections;
using UnityEngine;

public class LinkDrag : LinkDisplay, IDragLink
{
    [Header("Drag Settings")]
    public float Xlimit;
    public float Ylimit;

    //Used By LinkManager
    public Func<LinkSO, LinkDisplay, IEnumerator> OnDeploy;
    private float yaxis, xaxis;
    private Vector3 initial_position;

    [SerializeField] private bool locked;
    public bool Locked { get => locked; set => locked = value; }
    private void Awake()
    {
        initial_position = transform.position;
    }
    private void OnMouseDrag()
    {
        if (Locked || !_isActive) return;
        float YMouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
        float XMouseInput = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        yaxis = Mathf.Clamp(YMouseInput, initial_position.y, initial_position.y + Ylimit);
        xaxis = Mathf.Clamp(XMouseInput, initial_position.x, initial_position.x + Xlimit);
        this.transform.position = new Vector2(xaxis, yaxis);
    }
    private void OnMouseUp()
    {
        if (Locked || !_isActive) return;
        if (initial_position.y + Ylimit == transform.position.y)
        {
            SetOFF();
            StartCoroutine(OnDeploy?.Invoke(_linkdata, this));
        }
        transform.position = initial_position;
    }
}
