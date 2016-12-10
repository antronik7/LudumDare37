using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public Button[] ListLevel;
    private int indexBouton = 0;
	// Use this for initialization
	void Start () {
        ListLevel[0].Select();
	}
	
	// Update is called once per frame
	void Update () {


        
    }

    public void OnClick(string levelname)
    {
        print(levelname);
        if (levelname == "Exit")
            Application.Quit();
        Application.LoadLevel(levelname);
    }

    public void OnMove()
    {    
        indexBouton++;
        if (indexBouton >= ListLevel.Length - 1) indexBouton = 0;
        ListLevel[indexBouton].Select();
    }

}
