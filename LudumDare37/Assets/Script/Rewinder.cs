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
    public static void addTranslation(Vector3 playerPos, Vector3 roomPos, Vector3 cameraPos, bool isGround)
    {
        Scorer.instance.addScoreValue(2, 1);
        listState.Push(new RewindInfo(playerPos, 0, roomPos, cameraPos, isGround));
    }
    public static void addRotation(Vector3 playerPos, int dir, Vector3 cameraPos, bool isGround)
    {
        Scorer.instance.addScoreValue(2, 1);
        listState.Push(new RewindInfo(playerPos, 1, dir, cameraPos, isGround));
    }
    public static void addSymetrie(Vector3 playerPos, Vector3 cameraPos, bool isGround)
    {
        Scorer.instance.addScoreValue(2, 1);
        listState.Push(new RewindInfo(playerPos, 2, cameraPos, isGround));
    }
    

    public static void addSpawn(Vector3 playerPos, Vector3 cameraPos, bool isGround)
    {
        listState.Push(new RewindInfo(playerPos, 3, cameraPos, isGround));
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
            if(listState.Count == 1)
            {
                listState.Peek().rewind();
            }
            else
            {
                Scorer.instance.addScoreValue(2, -1);
                listState.Pop().rewind();
            }
        }
    }
}

