using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class New_Game_GUI : Hex_GUI {

	// GUI Styles
	private GUIStyle DifficultyLabel;
	private GUIStyle HeaderLabel;
	private GUIStyle HardToggle;
	private GUIStyle NormalToggle;
	private GUIStyle EasyToggle;
	private GUIStyle EasyWindow;
	private GUIStyle NormalWindow;
	private GUIStyle HardWindow;
	private GUIStyle OKButton;

	// Difficulty Window
	private static Rect difficultyWindow = new Rect(41, 57, 878, 486);
	private string difficulty;
	private bool[] difficultyBool;

	// Awake
	private void Awake(){
		difficulty = "normal";
		difficultyBool = new bool[3] {false, true, false};
	}

	// Load Skin
	protected override void LoadSkin(){
		skin = (GUISkin)Resources.Load("Skins/NewGameSkin");
	}

	// Load Styles
	protected override void LoadStyles(){
		DifficultyLabel = skin.GetStyle("DifficultyLabel");
		HeaderLabel = skin.GetStyle ("HeaderLabel");
		HardToggle = skin.GetStyle ("HardToggle");
		NormalToggle = skin.GetStyle ("NormalToggle");
		EasyToggle = skin.GetStyle ("EasyToggle");
		HardWindow = skin.GetStyle ("HardWindow");
		NormalWindow = skin.GetStyle ("NormalWindow");
		EasyWindow = skin.GetStyle ("EasyWindow");
		OKButton = skin.GetStyle ("OKButton");
	}

	// Load GUI
	protected override void LoadGUI(){
		CreateDifficultyWindow();
	}

	// Load Difficulty Window
	private void CreateDifficultyWindow(){
		if (difficultyBool[0])
			difficultyWindow = GUI.Window(0, difficultyWindow, CreateDifficultyItems, "", EasyWindow);
		else if (difficultyBool[1])
			difficultyWindow = GUI.Window(1, difficultyWindow, CreateDifficultyItems, "", NormalWindow);
		else if (difficultyBool[2])
			difficultyWindow = GUI.Window(2, difficultyWindow, CreateDifficultyItems, "", HardWindow);
	}

	// Load Difficulty Items
	private void CreateDifficultyItems(int windowID){
		GUI.Label(new Rect (21, 17, 376, 47), "SELECT DIFFICULTY", HeaderLabel);

		CreateDifficultyToggles();

		if (difficultyBool[0])
			GUI.Label(new Rect(61, 304, 756, 107), "The galaxy is your playground and its people your\nplaythings. Enemies cower in your presence and\nplanets bow to your will. But is any of this for real...?", DifficultyLabel);
		else if (difficultyBool[1])
			GUI.Label(new Rect(61, 304, 756, 107), "Your foes are as numerous as they are deadly.\nEntire worlds stand against you. Many trials lie ahead,\nbut a firm resolve will give you the power to write history.", DifficultyLabel);
		else if (difficultyBool[2])
			GUI.Label(new Rect(61, 304, 756, 107), "A black wind blows across space as a massive army\nemerges from the depths of the abyss. This time, all the\nlights in the sky are your enemy. Are you prepared?", DifficultyLabel);

		if (GUI.Button(new Rect(490, 427, 184, 47), "OK", OKButton)){
			PlayerPrefs.SetString("difficulty", difficulty);
			Application.LoadLevel("Main Menu");
		}
		if (GUI.Button(new Rect(684, 427, 184, 47), "CANCEL")){
			guiController.ReplaceGUI("Title_GUI");
		}
	}

	// Create Difficulty Toggles
	private void CreateDifficultyToggles(){
		CreateDifficultyToggle(new Rect (61, 82, 204, 204),  ref difficultyBool, 0, ref EasyToggle,   "easy");
		CreateDifficultyToggle(new Rect (337, 82, 204, 204), ref difficultyBool, 1, ref NormalToggle, "normal");
		CreateDifficultyToggle(new Rect (613, 82, 204, 204), ref difficultyBool, 2, ref HardToggle,   "hard");
	}

	// Create Difficulty Toggle
	private void CreateDifficultyToggle(Rect newRect, ref bool[] difficultyBool, int num,
	                                    ref GUIStyle difficultyToggle, string difficulty){
		if (GUI.Toggle(newRect, difficultyBool[num], "", difficultyToggle) != difficultyBool[num]){
			for (int i = 0; i < difficultyBool.Length; ++i){
				if (i == num)
					difficultyBool[i] = true;
				else
					difficultyBool[i] = false;
				this.difficulty = difficulty;
			}
		}
	}
}