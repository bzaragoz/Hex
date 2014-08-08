using System.IO;
using UnityEngine;
using System.Collections;

public class GUI_Controller : MonoBehaviour {

	// Current GUI
	private string currentGUI;

	// ATTACH DEFAULT GUI ON START
	void Start (){
		string currentScene = Application.loadedLevelName;

		switch (currentScene) {
			case "Main Menu":
				AttachGUI("Title_GUI");		break;
		}
	}

	// ATTACH GUI
	public void AttachGUI(string scriptName){
		this.gameObject.AddComponent(scriptName);
		currentGUI = scriptName;
	}

	// REPLACE GUI
	public void ReplaceGUI(string scriptName){
		Destroy(this.GetComponent(currentGUI));
		AttachGUI(scriptName);
	}

	// SWITCH GUI
	public IEnumerator SwitchGUI(string scriptName, float waitTime){
		yield return new WaitForSeconds(waitTime);
		Destroy(this.GetComponent(currentGUI));
		AttachGUI(scriptName);
	}
}