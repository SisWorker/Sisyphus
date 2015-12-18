using UnityEngine;
using System.Collections;


public class gameBoard : MonoBehaviour {

    string levelName;
	// Use this for initialization
	void Start () {
        levelName = Application.loadedLevelName;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player") || other.CompareTag("RockContact")) {
            Application.LoadLevel(levelName);
        }
    } 

}
