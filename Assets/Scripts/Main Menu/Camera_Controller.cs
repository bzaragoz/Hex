using UnityEngine;
using System.Collections;

public class Camera_Controller : MonoBehaviour {

	// Current Camera
	private string currentCamera;

	// Attach default camera on start
	void Start () {
		string currentScene = Application.loadedLevelName;
		
		switch (currentScene) {
		case "Main Menu":
			AttachCamera("Title_Camera");		break;
		}
	}

	// Attach camera
	public void AttachCamera(string scriptName){
		this.gameObject.AddComponent(scriptName);
		currentCamera = scriptName;
	}
	
	// Replace camera
	public void ReplaceCamera(string scriptName){
		Destroy(this.GetComponent(currentCamera));
		AttachCamera(scriptName);
	}
}