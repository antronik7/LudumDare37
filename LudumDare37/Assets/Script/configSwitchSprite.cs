using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class configSwitchSprite : MonoBehaviour {

	public Sprite controllerSprite;
	public Sprite keyboardSprite;

	public bool isUI;


	void Start () {
		if (!isUI) {
			if (Scorer.instance.getScoreValue ("controllerChoice") == 0)//keyboard
			GetComponent<SpriteRenderer> ().sprite = keyboardSprite;
			else {//controller
				GetComponent<SpriteRenderer> ().sprite = controllerSprite;
			}
		} else {
			if (Scorer.instance.getScoreValue ("controllerChoice") == 0)//keyboard
				GetComponent<Image> ().sprite = keyboardSprite;
			else {//controller
				GetComponent<Image> ().sprite = controllerSprite;
			}
	
		}
	}
}
