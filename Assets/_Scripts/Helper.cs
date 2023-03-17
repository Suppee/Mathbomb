using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    private static readonly Dictionary<float, WaitForSeconds> waitDict = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWait(float time) {
        if(waitDict.TryGetValue(time, out var wait)) return wait;

        waitDict[time] = new WaitForSeconds(time);
        return waitDict[time];
    }
}