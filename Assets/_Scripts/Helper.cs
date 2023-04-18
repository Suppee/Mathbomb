using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper {
    private static readonly Dictionary<float, WaitForSeconds> waitDict = new Dictionary<float, WaitForSeconds>();
    public static WaitForSeconds GetWait(float time) {
        if(waitDict.TryGetValue(time, out var wait)) return wait;

        waitDict[time] = new WaitForSeconds(time);
        return waitDict[time];
    }

    public static void UpdateQueue<T>(this Queue<T> q, T item, int maxCapacity) {
        q.Enqueue(item);
        if(q.Count > maxCapacity) {
            q.Dequeue();
        }
    }

    public static Quaternion AverageQuaternion(this Queue<Quaternion> q) {
        float x = 0;
        float y = 0;
        float z = 0;

        foreach(Quaternion que in q) {
            x += que.eulerAngles.x;
            y += que.eulerAngles.y;
            z += que.eulerAngles.z;
        }

        x /= 10;
        y /= 10;
        z /= 10;

        Vector3 averageEuler = new Vector3(x, y, z);
        Quaternion averageRotation = Quaternion.Euler(averageEuler);

        return averageRotation;
    }

}
