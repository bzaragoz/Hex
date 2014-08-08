using UnityEngine;
using System.Collections;

public abstract class Hex_GUI : MonoBehaviour {

	protected GUI_Controller guiController;
	protected MenuCam_Controller camController;

	protected static float originalWidth = 960.0f;
	protected static float originalHeight = 600.0f;

	protected GUISkin skin;
	protected float guiAlpha;

	// Start
	protected void Start(){
		guiController = GameObject.Find ("GUI").GetComponent<GUI_Controller>();
		camController = GameObject.Find ("Main Camera").GetComponent<MenuCam_Controller>();
		LoadSkin();
		LoadStyles();
	}

	// On GUI
	protected void OnGUI(){
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, guiAlpha);
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

	// Fade In GUI
	protected IEnumerator FadeInGUI(float start, float end, float length, float waitTime){
		yield return new WaitForSeconds(waitTime);
		for (float i = 0.0f; i <= 1.0f; i+=Time.deltaTime*(1/length))
			yield return guiAlpha = Mathf.InverseLerp(start, end, i);
		guiAlpha = end;
	}

	// Fade Out GUI
	protected IEnumerator FadeOutGUI(float start, float end, float length){
		for (float i = 0.0f; i <= 1.0f; i+=Time.deltaTime*(1/length))
			yield return guiAlpha = Mathf.Lerp(start, end, i);
		guiAlpha = end;
	}

	protected void NoItems(int windowID){;}
	// Abstract
	protected abstract void LoadSkin();
	protected abstract void LoadStyles();
	protected abstract void LoadGUI();
}