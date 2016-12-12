using UnityEngine;
using System.Collections;

public class IntroductionCamera : MonoBehaviour {
    public GameObject pressToStart;

    void Update() {
        transform.position = new Vector3(PlayerController.instance.transform.position.x, PlayerController.instance.transform.position.y, transform.position.z);
        if (Input.anyKey) { 
            ExitController.instance.transform.position = new Vector3(PlayerController.instance.transform.position.x, PlayerController.instance.transform.position.y-6, ExitController.instance.transform.position.z);
            pressToStart.SetActive(false);
        }
    }
}
