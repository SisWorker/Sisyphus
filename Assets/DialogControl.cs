using UnityEngine;
using System.Collections;

public class DialogControl : MonoBehaviour {

	public bool dialogOn;
	public GameObject DialogCanvas;
	// Use this for initialization

	// Update is called once per frame
	void Update () {
		if (dialogOn) {
			DialogCanvas.SetActive (true);
		} else {
			DialogCanvas.SetActive (false);
		}
	
	}
	void showDialog()
	{
		dialogOn = true;
	}
	void hideDialog()
	{
		dialogOn = false;
	}
}
