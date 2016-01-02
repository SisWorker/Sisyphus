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
		dialogs = readFile (fileDir);
		Debug.Log("dialogs length: "+dialogs.Count);
		curDialog = new List<string> ();
		curDialog.Add ("No Content");
		dialogText = DialogCanvas.GetComponentInChildren<Text> ();





	}
	// Update is called once per frame
	void Update () {
		dialogSwitch (dialogOn);
		dialogText.text = curDialog [curSentenceIndex];



	
	}

	//switch to turn on or off the dialog canvas according to the value of boolean: dialogOn
	void dialogSwitch(bool dialogOn){
		if (dialogOn) {
			DialogCanvas.SetActive (true);
		} else {
			DialogCanvas.SetActive (false);
		}
	}


	//the text file reading function
	//read the dialog script into the data structure, so it can be called later
	//the List<List<string>> will be initialized such that when text file is empty or incorrect, it will display a warning message

	List<List<string>> readFile(string fileDir){
		//read 
		int loopcount = 0;
		List<List<string>> result = new List<List<string>>();
		StreamReader reader = new StreamReader(fileDir);
		string line = reader.ReadLine();
		while(line!=null){
			if(line.Length!=0){
				List<string> dialog = new List<string>();
				while (line.Length!=0) {
					dialog.Add (line);
					line = reader.ReadLine ();
					if (line == null) {
						result.Add (dialog);
						return result;
					}
				}
				result.Add (dialog);
			}
			line = reader.ReadLine ();
		
		}
		return result;

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
