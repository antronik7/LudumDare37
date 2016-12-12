using UnityEngine;
using System.Collections;


public class RewindInfo
{
    Vector3 playerPos;
    int type;//0 translate; 1 rotate; 2 symetri
    Vector3 roomPos;
    int dir;
    Vector3 cameraPos;


    bool gotKey;
    bool gotStar;
    public RewindInfo(Vector3 playerPos, int type, Vector3 cameraPos)
    {
        this.playerPos = playerPos;
        this.type = type;
        this.cameraPos = cameraPos;
        this.gotKey = false;
        this.gotStar = false;
    }
    public RewindInfo(Vector3 playerPos, int type, Vector3 roomPos, Vector3 cameraPos)
    {
        this.playerPos = playerPos;
        this.type = type;
        this.roomPos = roomPos;
        this.cameraPos = cameraPos;
        this.gotKey = false;
        this.gotStar = false;
    }
    public RewindInfo(Vector3 playerPos, int type, int dir, Vector3 cameraPos)
    {
        this.playerPos = playerPos;
        this.type = type;
        this.dir = dir;
        this.cameraPos = cameraPos;
        this.gotKey = false;
        this.gotStar = false;
    }

    public void rewind()
    {
        switch (type)
        {
            case 0:
                RessourceManager.instance.NbrTranslation += 1;
                OneRoomController.instance.moveTo(this.roomPos);
                PlayerController.instance.moveTo(this.playerPos);
                AudioController.instance.playClip(3);
                break;
            case 1:
                RessourceManager.instance.NbrRotation += 1;
                OneRoomController.instance.rotateTo(dir);
                PlayerController.instance.moveTo(this.playerPos);
                AudioController.instance.playClip(5);
                break;
            case 2:
                RessourceManager.instance.NbrSymetrie += 1;
                OneRoomController.instance.doSymetrie();
                PlayerController.instance.moveTo(this.playerPos);
                AudioController.instance.playClip(7);
                break;
            case 3:
                PlayerController.instance.moveTo(this.playerPos);
                AudioController.instance.playClip(3);
                break;
            default:
                break;
        }
        CameraController.instance.transform.position = cameraPos;
        resetKey();
        //resetStar();
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