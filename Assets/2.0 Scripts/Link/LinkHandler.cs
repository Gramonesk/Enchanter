using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//public class LinkHandler
//{
//    //Belom ngecek mati atau enggaknya
//    public static IEnumerator Activate(DeployData deploydata, Card target)
//    {
//        var targets = deploydata.targets;
//        bool AbleToAbility = deploydata.IsAbility;
//        foreach (var toTarget in targets)
//        {
//            if (AbleToAbility) Inflict(deploydata.link.Skill, toTarget);
//            else Inflict(deploydata.link.Basic, toTarget);
//        }
//        yield return null;
//    }
//    public static void Inflict(List<Effect> effects, Card target)
//    {
//        foreach (var effect in effects)
//        {
//            effect.Activate(target);
//        }
//    }
//}
