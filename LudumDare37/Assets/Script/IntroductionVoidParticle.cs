using UnityEngine;
using System.Collections;

public class IntroductionVoidParticle : MonoBehaviour {
    void Update()
    {
        transform.position = new Vector3(PlayerController.instance.transform.position.x, PlayerController.instance.transform.position.y-5, transform.position.z);
    }
}
