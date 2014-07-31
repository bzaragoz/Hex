using UnityEngine;
using System.Collections;

public abstract class Hex_GUI : MonoBehaviour {

	protected GUI_Controller guiController;

	protected static float originalWidth = 960.0f;
	protected static float originalHeight = 600.0f;

	void Start(){
		guiController = GameObject.Find ("GUI").GetComponent<GUI_Controller>();
		LoadSkin();
		LoadStyles();
	}

	protected abstract void LoadSkin();
	protected abstract void LoadStyles();
}