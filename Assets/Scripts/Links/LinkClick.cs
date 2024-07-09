using System;

public class LinkClick : LinkDisplay
{
    public Action<LinkClick> OnCancel;
    private void OnMouseDown()
    {
        if (_isActive) OnCancel?.Invoke(this);
    }
}
