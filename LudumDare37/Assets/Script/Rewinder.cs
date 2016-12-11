using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Rewinder : MonoBehaviour {
    static Stack<RewindInfo> listState;
    GameObject OneRoom;

    public static KeyLockController keyLockController;
    public static StarController starController;

    void Awake()
    {
        listState = new Stack<RewindInfo>();
        OneRoom = GameObject.FindGameObjectWithTag("Room");
        keyLockController = KeyLockController.instance;
        starController = StarController.instance;
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

    public static void gotKey()
    {
        if(listState.Count != 0)
        {
            listState.Peek().setKey();
        }
    }

    public static void gotStar()
    {
        if (listState.Count != 0)
        {
            listState.Peek().setStar();
        }
    }

    public static void rewind()
    {
        if(listState.Count != 0)
        {
            listState.Pop().rewind();
        }
    }
}

