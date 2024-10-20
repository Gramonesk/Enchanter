using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that provides the battle and turn mechanics (can be tweaked anytime)
/// BattleHandler consist of setting the order and do battle calculation
/// </summary>
public class BattleHandler
{
    public static List<DeployData> Player_Deploydata;
    public static List<DeployData> Enemy_Deploydata;
    //public static void Run()
    //{
    //    var PlayerCount = Player_Deploydata.Count;
    //    var EnemyCount = Enemy_Deploydata.Count;
    //    int index = PlayerCount < EnemyCount ? PlayerCount : EnemyCount;
    //    for (int i = 0; i < index; i++)
    //    {
    //        if (i >= EnemyCount || ( i < PlayerCount && Player_Deploydata[i].link.user.Dexterity > Enemy_Deploydata[i].link.user.Dexterity))
    //        {
    //            Do(Player_Deploydata[i]);
    //            Do(Enemy_Deploydata[i]);
    //        }
    //        else
    //        {
    //            Do(Enemy_Deploydata[i]);
    //            Do(Player_Deploydata[i]);
    //        }
    //    }
    //    for(int i = index; i < PlayerCount; i++)
    //    {
    //        Do(Player_Deploydata[i]);
    //    }
    //    for (int i = index; i < EnemyCount; i++)
    //    {
    //        Do(Enemy_Deploydata[i]);
    //    }
    //}
    //static void Do(DeployData user)
    //{
    //    user.link.Activate(user.targets, user.IsAbility);
    //}
    public static IEnumerator RunCoroutine()
    {
        var PlayerCount = Player_Deploydata.Count;
        var EnemyCount = Enemy_Deploydata.Count;
        int index = PlayerCount < EnemyCount ? PlayerCount : EnemyCount;
        for (int i = 0; i < index; i++)
        {
            if (i >= EnemyCount || (i < PlayerCount && Player_Deploydata[i].link.user.Dexterity > Enemy_Deploydata[i].link.user.Dexterity))
            {
                yield return DoCoroutine(Player_Deploydata[i]);
                yield return DoCoroutine(Enemy_Deploydata[i]);
            }
            else
            {
                yield return DoCoroutine(Enemy_Deploydata[i]);
                yield return DoCoroutine(Player_Deploydata[i]);
            }
        }
        for (int i = index; i < PlayerCount; i++)
        {
            yield return DoCoroutine(Player_Deploydata[i]);
        }
        for (int i = index; i < EnemyCount; i++)
        {
            yield return DoCoroutine(Enemy_Deploydata[i]);
        }
    }
    public static IEnumerator DoCoroutine(DeployData user)
    {
        yield return user.link.ActivateCTN(user.targets, user.isAbility);
    }
}
