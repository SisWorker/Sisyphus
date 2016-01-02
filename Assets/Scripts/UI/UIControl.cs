using UnityEngine;
using System.Collections;

public class UIControl : MonoBehaviour {

	public void changeScene(string sceneName)
	{
		Application.LoadLevel (sceneName);
	}
}
