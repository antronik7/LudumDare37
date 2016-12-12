using UnityEngine;
using System.Collections;

public class ButtonsMusic : MonoBehaviour
{

    public void valid()
    {
        AudioController.instance.playClip(15);
    }

    public void onSelect()
    {
        AudioController.instance.playClip(14);
    }
}
