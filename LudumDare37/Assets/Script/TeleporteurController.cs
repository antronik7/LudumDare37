using UnityEngine;
using System.Collections;

public class TeleporteurController : MonoBehaviour {

    private GameObject Player;
    public GameObject SortieWrap;
	[HideInInspector]public static bool canTeleport = true;

    void OnTriggerEnter2D(Collider2D other)
    {
		if ((other.tag == "Player") && (canTeleport))
        {
            AudioController.instance.playClip(9);
            Player = other.gameObject;
			canTeleport = false;
			Player.transform.position = SortieWrap.transform.position;
		} else if ((other.tag == "Player") && (!canTeleport))
        {
            AudioController.instance.playClip(9);
            //StartCoroutine(waitBeforeCanTeleport());
			canTeleport = true;
		}
    }

	public IEnumerator waitBeforeCanTeleport(){
		yield return new WaitForSeconds (0.75f);
		canTeleport = true;
	}

}
