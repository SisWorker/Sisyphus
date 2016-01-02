using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.UI;

/*

This script will be responsible for reading a text file and store it in a List<List<string>>

Provide a series of functions for dialog triggers to trigger

Dialog triggers can come in variety of forms: physical triggerEnter, time based, tutorial, conversation based...

This will be the primary Dialog Management Script

*/



public class DialogControl : MonoBehaviour {

	public bool dialogOn;
	public GameObject DialogCanvas;
	public string fileDir;

	private List<List<string>> dialogs;
	private Text dialogText; 
	private List<string> curDialog;
	private int curSentenceIndex;

	// Use this for initialization
	void Start(){
		dialogs = new List<List<string>> ();
		curDialog = new List<string> ();
		curSentenceIndex = 0;
		readFile (fileDir);



	}
	// Update is called once per frame
	void Update () {
		dialogText = DialogCanvas.GetComponentInChildren<Text> ();
		dialogSwitch (dialogOn);
		if (curSentenceIndex < curDialog.Count && curSentenceIndex >= 0 && curDialog != null) {
			dialogText.text = curDialog [curSentenceIndex];
		}



	
	}

	//switch to turn on or off the dialog canvas according to the value of boolean: dialogOn
	void dialogSwitch(bool dialogOn){
		if (dialogOn) {
			Time.timeScale = 0f;
			DialogCanvas.SetActive (true);
		} else {
			DialogCanvas.SetActive (false);
			Time.timeScale = 1f;
		}
	}


	//the text file reading function
	//read the dialog script into the data structure, so it can be called later
	//the List<List<string>> will be initialized such that when text file is empty or incorrect, it will display a warning message

	void readFile(string fileDir){
		//read 
		StreamReader reader = new StreamReader(fileDir);
		string line;
		while((line = reader.ReadLine())!=null){
			if(!line.Equals("")){
				List<string> dialog = new List<string>();
				while (!line.Equals("")) {
					dialog.Add (line);
					line = reader.ReadLine ();
					if (line == null)
						return;
				}
				dialogs.Add (dialog);
			}
			line = reader.ReadLine ();
		}
	}


	//this function calls a speciific dialog stored inside of the data structure

	public void invokeDialog(int index){
		if (index >= dialogs.Count||index<0) {
			Debug.Log ("index out of dialogs range");
			return;
		}
		curSentenceIndex = 0;
		dialogOn = true;
		curDialog = dialogs [index];
		dialogText.text = curDialog [curSentenceIndex];
	}

	//increment curSentenceIndex once "Next" button is clicked, so the displayed text is updated

	public void procceed(){
		curSentenceIndex++;
		if (curSentenceIndex >= curDialog.Count) {
			curSentenceIndex = 0;
			dialogOn = false;
		}
	}



}
