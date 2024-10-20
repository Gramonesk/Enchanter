using System;
using UnityEngine;

public class Clickable : MonoBehaviour
{
    public Action OnClicked { get; set; }

    public void OnMouseEnter()
    {
        OnClicked?.Invoke();
    }
}
