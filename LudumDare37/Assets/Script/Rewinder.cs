using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rewinder : MonoBehaviour {
    static Stack<RewindInfo> listState;
    public GameObject OneRoom;

    void Awake()
    {
        listState = new Stack<RewindInfo>();
    }

    public static void addTranslation(Vector3 playerPos, Vector3 roomPos)
    {
        listState.Push(new RewindInfo(playerPos, 0, roomPos));
    }
    static void addRotation(Vector3 playerPos, int dir)
    {
        listState.Push(new RewindInfo(playerPos, 1, dir));
    }
    static void addMiror(Vector3 playerPos)
    {
        listState.Push(new RewindInfo(playerPos, 2));
    }

    public static void rewind()
    {
        listState.Pop().rewind();
    }
}

