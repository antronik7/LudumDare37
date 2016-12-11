using UnityEngine;
using System.Collections;


public class RewindInfo
{
    Vector3 playerPos;
    int type;//0 translate; 1 rotate; 2 mirror
    Vector3 roomPos;
    int dir;
    public RewindInfo(Vector3 playerPos, int type)
    {
        this.playerPos = playerPos;
        this.type = type;
    }
    public RewindInfo(Vector3 playerPos, int type, Vector3 roomPos)
    {
        this.playerPos = playerPos;
        this.type = type;
        this.roomPos = roomPos;
    }
    public RewindInfo(Vector3 playerPos, int type, int dir)
    {
        this.playerPos = playerPos;
        this.type = type;
        this.dir = dir;
    }

    public void rewind()
    {
        switch (type)
        {
            case 0:
                OneRoomController.instance.moveTo(this.roomPos);
                PlayerController.instance.moveTo(this.playerPos);
                break;
            default:
                break;
        }

    }
}