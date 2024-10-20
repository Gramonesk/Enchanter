using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static int turn = 1;
    public float time, Timer;
    public bool playerReady, enemyReady;
    public void Preparation()
    {
        if(GameManager.SM.state == GameState.Preparation)
        {
            StartCoroutine(StartTimer());
        }
    }
    private IEnumerator StartTimer()
    {
        while((time < Timer) && !(playerReady && enemyReady))
        {
            yield return null;
        }
        //BattleHandler.Start
    }
}
