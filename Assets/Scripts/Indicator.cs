using System;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    [SerializeField] float timeInterval;
    public int ready; 
    float time;
    public Action onReady;
    private void StartPreparation()
    {
        time = Time.time + timeInterval;
    }
    private void UpdatePreparation()
    {
        if(time - Time.time < 0 || ready > 0)
        {
            onReady?.Invoke();
        }
    }
}
