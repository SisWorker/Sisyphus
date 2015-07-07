using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class UIDialog : MonoBehaviour {

	public string dialogString;
	// Use this for initialization
	void Start ()
	{
		dialogString = GameObject.Find ("DialogText").GetComponent<Text> ().text;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
