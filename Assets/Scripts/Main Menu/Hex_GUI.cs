using UnityEngine;
using System.Collections;

public abstract class Hex_GUI : MonoBehaviour {

	protected GUI_Controller guiController;

	protected static float originalWidth = 960.0f;
	protected static float originalHeight = 600.0f;

	protected GUISkin skin;

	// Start
	protected void Start(){
		guiController = GameObject.Find ("GUI").GetComponent<GUI_Controller>();
		LoadSkin();
		LoadStyles();
	}

	// On GUI
	protected void OnGUI(){
		GUI.skin = skin;
		LoadGUIMatrix();
		LoadGUI();
		GUI.matrix = Matrix4x4.identity;
	}

	// Load GUI Matrix
	protected void LoadGUIMatrix(){
		Vector2 ratio = new Vector2(Screen.width/originalWidth , Screen.height/originalHeight );
		Matrix4x4 guiMatrix = Matrix4x4.identity;
		guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
		GUI.matrix = guiMatrix;
	}

	protected void NoItems(int windowID){;}

	// Abstract
	protected abstract void LoadSkin();
	protected abstract void LoadStyles();
	protected abstract void LoadGUI();
}