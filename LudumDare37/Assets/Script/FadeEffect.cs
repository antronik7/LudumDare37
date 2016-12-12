using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeEffect : MonoBehaviour
{
	private Image imgFade;

	void Start() {
		imgFade = GetComponentInChildren<Image> ();
		fadeFX (0f, Color.black, 3f);
	}

	public void fadeFX(float alpha, Color color, float fadeTime){
		print ("ok");
		imgFade.color = color;
		imgFade.CrossFadeAlpha (alpha, fadeTime, true);
	}

	public void startFadeInOut(Color color, float fadeTime){
		StartCoroutine (fadeInOut (color, fadeTime));
	}

	IEnumerator fadeInOut(Color color,float fadeTime){
		float alpha = 1;
		imgFade.color = color;
		imgFade.CrossFadeAlpha (alpha, fadeTime, true);
		yield return new WaitForSeconds (fadeTime);
		alpha = 0;
		imgFade.CrossFadeAlpha (alpha, fadeTime, true);
	}



}