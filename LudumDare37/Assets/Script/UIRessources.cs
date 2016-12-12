using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIRessources : MonoBehaviour {

	private Text[] textReceipteur;
	private RessourceManager ressources;

	void Start () {
		textReceipteur = this.GetComponentsInChildren<Text> ();
		ressources = GameObject.FindGameObjectWithTag ("Player").GetComponent<RessourceManager>();
		updateTextUI ();
	}

	void Update(){
		updateTextUI ();
	}

	public void updateTextUI(){
		textReceipteur [0].text = ressources.NbrTranslation.ToString();
		textReceipteur [1].text = ressources.NbrRotation.ToString();
		textReceipteur [2].text = ressources.NbrSymetrie.ToString();
	}
}
