using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class New_Game_GUI : Hex_GUI {

	// GUI Skin and Styles
	public GUISkin TitleSkin;
	public GUISkin NewGameSkin = (GUISkin)Resources.Load("Skins/NewGameSkin");
	public GUIStyle DifficultyLabel;
	public GUIStyle HeaderLabel;
	public GUIStyle HardToggle;
	public GUIStyle NormalToggle;
	public GUIStyle EasyToggle;
	public GUIStyle EasyWindow;
	public GUIStyle NormalWindow;
	public GUIStyle HardWindow;
	public GUIStyle OKButton;

	// Difficulty Window Textures
	public Texture2D hardTexture = (Texture2D)Resources.Load ("Textures/hard");

	// Window Rectangles
	Rect difficultyWindow = new Rect(41, 57, 878, 486);

	// Window Booleans
	private bool hardBool = false;
	private bool normalBool = true;
	private bool easyBool = false;

	// String Variables
	string difficulty = "normal";

	// Load Skin
	protected override void LoadSkin(){
		TitleSkin = (GUISkin)Resources.Load ("Skins/TitleSkin");
	}

	// Load Styles
	protected override void LoadStyles(){
		DifficultyLabel = NewGameSkin.GetStyle("DifficultyLabel");
		HeaderLabel = NewGameSkin.GetStyle ("HeaderLabel");
		HardToggle = NewGameSkin.GetStyle ("HardToggle");
		NormalToggle = NewGameSkin.GetStyle ("NormalToggle");
		EasyToggle = NewGameSkin.GetStyle ("EasyToggle");
		HardWindow = NewGameSkin.GetStyle ("HardWindow");
		NormalWindow = NewGameSkin.GetStyle ("NormalWindow");
		EasyWindow = NewGameSkin.GetStyle ("EasyWindow");
		OKButton = NewGameSkin.GetStyle ("OKButton");
	}

	// Run on GUI
	void OnGUI () {
		GUI.skin = NewGameSkin;

		// Set matrix
		Vector2 ratio = new Vector2(Screen.width/originalWidth , Screen.height/originalHeight );
		Matrix4x4 guiMatrix = Matrix4x4.identity;
		guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
		GUI.matrix = guiMatrix;

		if (easyBool)
			difficultyWindow = GUI.Window(0, difficultyWindow, difficultyItems, "", EasyWindow);
		else if (normalBool)
			difficultyWindow = GUI.Window(1, difficultyWindow, difficultyItems, "", NormalWindow);
		else if (hardBool)
			difficultyWindow = GUI.Window(2, difficultyWindow, difficultyItems, "", HardWindow);

		// Reset matrix
		GUI.matrix = Matrix4x4.identity;
	}

	// Difficulty
	void difficultyItems(int windowID){
		GUI.Label(new Rect (21, 17, 376, 47), "SELECT DIFFICULTY", HeaderLabel);
		if (GUI.Toggle(new Rect(61, 82, 204, 204), easyBool, "", EasyToggle) != easyBool){
			if (!easyBool)
				easyBool = true;
			if (normalBool)
				normalBool = false;
			if (hardBool)
				hardBool = false;
			difficulty = "easy";
		}
		if (GUI.Toggle(new Rect(337, 82, 204, 204), normalBool, "", NormalToggle) != normalBool){
			if (!normalBool)
				normalBool = true;
			if (easyBool)
				easyBool = false;
			if (hardBool)
				hardBool = false;
			difficulty = "normal";
		}
		if (GUI.Toggle(new Rect(613, 82, 204, 204), hardBool, "", HardToggle) != hardBool){
			if (!hardBool)
				hardBool = true;
			if (easyBool)
				easyBool = false;
			if (normalBool)
				normalBool = false;
			difficulty = "hard";
		}
		if (difficulty == "easy")
			GUI.Label(new Rect(61, 304, 756, 107), "The galaxy is your playground and its people your\nplaythings. Enemies cower in your presence and\nplanets bow to your will. But is any of this for real...?", DifficultyLabel);
		else if (difficulty == "normal")
			GUI.Label(new Rect(61, 304, 756, 107), "Your foes are as numerous as they are deadly.\nEntire worlds stand against you. Many trials lie ahead,\nbut a firm resolve will give you the power to write history.", DifficultyLabel);
		else if (difficulty == "hard")
			GUI.Label(new Rect(61, 304, 756, 107), "A black wind blows across space as a massive army\nemerges from the depths of the abyss. This time, all the\nlights in the sky are your enemy. Are you prepared?", DifficultyLabel);
		if (GUI.Button(new Rect(490, 427, 184, 47), "OK", OKButton)){
			PlayerPrefs.SetString("difficulty", difficulty);
			Application.LoadLevel("Main Menu");
		}
		if (GUI.Button(new Rect(684, 427, 184, 47), "CANCEL")){
			guiController.ReplaceGUI("Title_GUI");
		}
	}
}
