using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public Animator animator;
    public Action<DamageText> OnRelease;
    IEnumerator Start()
    {
        animator.Play("Text");
        yield return new WaitUntil(() => animator.name.Equals("Text"));
        yield return new WaitUntil(() => !animator.name.Equals("Text"));
        OnRelease?.Invoke(this);
    }
}
