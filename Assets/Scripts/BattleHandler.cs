using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that provides the battle and turn mechanics (can be tweaked anytime)
/// BattleHandler consist of setting the order and do battle calculation
/// </summary>
public class BattleHandler
{
    public static List<DeployData> Player_Deploydata = new();
    public static List<DeployData> Enemy_Deploydata = new();

    public static IEnumerator QueueRun()
    {
        yield return new WaitUntil(() => Enemy_Deploydata.Count > 0 && Player_Deploydata.Count > 0);
        yield return Run();
        Enemy_Deploydata.Clear();
        Player_Deploydata.Clear();
    }
    public static IEnumerator Run()
    {
        var PlayerCount = Player_Deploydata.Count;
        var EnemyCount = Enemy_Deploydata.Count;
        int index = PlayerCount > EnemyCount ? PlayerCount : EnemyCount;
        for (int i = 0; i < index; i++)
        {
            if (i >= EnemyCount || ( i < PlayerCount && Player_Deploydata[i].data.user.Dexterity > Enemy_Deploydata[i].data.user.Dexterity))
            {
                yield return LinkHandler.Activate(Player_Deploydata[i], null);
                yield return LinkHandler.Activate(Enemy_Deploydata[i], null);
            }
            else
            {
                yield return LinkHandler.Activate(Enemy_Deploydata[i], null);
                yield return LinkHandler.Activate(Player_Deploydata[i], null);
            }
        }
    }
}
