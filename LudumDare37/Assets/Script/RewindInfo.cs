using UnityEngine;
using System.Collections;


public class RewindInfo
{
    Vector3 playerPos;
    int type;//0 translate; 1 rotate; 2 symetri
    Vector3 roomPos;
    int dir;

    bool gotKey;
    bool gotStar;
    public RewindInfo(Vector3 playerPos, int type)
    {
        this.playerPos = playerPos;
        this.type = type;
        this.gotKey = false;
        this.gotStar = false;
    }
    public RewindInfo(Vector3 playerPos, int type, Vector3 roomPos)
    {
        this.playerPos = playerPos;
        this.type = type;
        this.roomPos = roomPos;
        this.gotKey = false;
        this.gotStar = false;
    }
    public RewindInfo(Vector3 playerPos, int type, int dir)
    {
        this.playerPos = playerPos;
        this.type = type;
        this.dir = dir;
        this.gotKey = false;
        this.gotStar = false;
    }

    public void rewind()
    {
        switch (type)
        {
            case 0:
                OneRoomController.instance.moveTo(this.roomPos);
                PlayerController.instance.moveTo(this.playerPos);
                break;
            case 1:
                OneRoomController.instance.rotateTo(dir);
                PlayerController.instance.moveTo(this.playerPos);
                break;
            case 2:
                OneRoomController.instance.doSymetrie();
                PlayerController.instance.moveTo(this.playerPos);
                break;
            default:
                break;
        }
        resetKey();
        resetStar();
    }

    public void setKey()
    {
        this.gotKey = true;
    }

    public void setStar()
    {
        this.gotStar = true;
    }

    private void resetKey()
    {
        if (this.gotKey)
        {
            Rewinder.keyLockController.resetKey();
        }
    }

    private void resetStar()
    {
        if (this.gotStar)
        {
            Rewinder.starController.resetStar();
        }
    }
}