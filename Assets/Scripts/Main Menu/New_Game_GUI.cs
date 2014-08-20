using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class New_Game_GUI : Hex_GUI {

	// GUI Styles
	private static GUIStyle DifficultyLabel;
	private static GUIStyle HeaderLabel;
	private static GUIStyle HardToggle;
	private static GUIStyle NormalToggle;
	private static GUIStyle EasyToggle;
	private static GUIStyle EasyWindow;
	private static GUIStyle NormalWindow;
	private static GUIStyle HardWindow;
	private static GUIStyle OKButton;

	// Difficulty Window
	private static Rect difficultyWindow = new Rect(41, 57, 878, 486);
	private Dictionary<string, bool> difficultyBool = new Dictionary<string, bool>();
	private string difficulty = "normal";

	// Audio Sources
	private static AudioSource menuSelect;
	private static AudioSource menuHover;
	private static AudioSource cancelSelect;
	private string lastTooltip = "";

	// Awake
	private void Awake(){
		LoadDifficulties();
		LoadAudioSources();
		guiAlpha = 0.0f;
		StartCoroutine(FadeInGUI(0.0f, 1.0f, 1.0f, 0.0f));
	}

	// Load Difficulties
	private void LoadDifficulties(){
		difficultyBool.Add("easy", false);
		difficultyBool.Add("normal", true);
		difficultyBool.Add("hard", false);
	}

	// Load Audio Sources
	private void LoadAudioSources(){
		menuSelect = gameObject.AddComponent<AudioSource>();
		menuHover = gameObject.AddComponent<AudioSource>();
		cancelSelect = gameObject.AddComponent<AudioSource>();
		menuSelect.clip = Resources.Load("Music/Difficulty Select") as AudioClip;
		menuHover.clip = Resources.Load("Music/Difficulty Hover") as AudioClip;
		cancelSelect.clip = Resources.Load("Music/Cancel Select") as AudioClip;
	}

	// Set Difficulty
	private void SetDifficulty(string difficulty){
		List<string> keys = new List<string>(difficultyBool.Keys);
		
		foreach(string key in keys){
			if (key == difficulty)
				difficultyBool[key] = true;
			else
				difficultyBool[key] = false;
			this.difficulty = difficulty;
		}
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
		LoadDifficultyWindow();
	}

	// Load Difficulty Window
	private void LoadDifficultyWindow(){
		if (difficultyBool["easy"])
			difficultyWindow = GUI.Window(0, difficultyWindow, CreateDifficultyWindow, "", EasyWindow);
		else if (difficultyBool["normal"])
			difficultyWindow = GUI.Window(1, difficultyWindow, CreateDifficultyWindow, "", NormalWindow);
		else if (difficultyBool["hard"])
			difficultyWindow = GUI.Window(2, difficultyWindow, CreateDifficultyWindow, "", HardWindow);
	}

	// Create Difficulty Window
	private void CreateDifficultyWindow(int windowID){
		GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, guiAlpha);
		CreateHeaderLabel();
		CreateDifficultyToggles();
		CreateDescription();
		CreateOKButton();
		CreateCancelButton();

		if (Event.current.type == EventType.Repaint && GUI.tooltip != lastTooltip && guiAlpha == 1.0f) {
			if (lastTooltip != "")
				SendMessage("OnMouseOut", SendMessageOptions.DontRequireReceiver);
			if (GUI.tooltip != "" && GUI.tooltip != difficulty)
				SendMessage("OnMouseOver", SendMessageOptions.DontRequireReceiver);
			
			lastTooltip = GUI.tooltip;
		}
	}

	// Create Header Label
	private void CreateHeaderLabel(){
		GUI.Label(new Rect (21, 17, 376, 47), "SELECT DIFFICULTY", HeaderLabel);
	}

	// Create Difficulty Toggles
	private void CreateDifficultyToggles(){
		CreateDifficultyToggle(new Rect (61, 82, 204, 204),  ref EasyToggle,   "easy");
		CreateDifficultyToggle(new Rect (337, 82, 204, 204), ref NormalToggle, "normal");
		CreateDifficultyToggle(new Rect (613, 82, 204, 204), ref HardToggle,   "hard");
	}

	// Create Difficulty Toggle
	private void CreateDifficultyToggle(Rect newRect, ref GUIStyle difficultyToggle, string difficulty){
		if (GUI.Toggle (newRect, difficultyBool[difficulty], new GUIContent("", difficulty), difficultyToggle) != difficultyBool[difficulty] && guiAlpha == 1.0f) {
			SetDifficulty (difficulty);
			menuSelect.Play();
		}
	}

	// Menu Hover Sound
	private void OnMouseOver(){
		menuHover.Play();
	}

	private void OnMouseOut(){

	}

	// Create Description
	private void CreateDescription(){
		if (difficultyBool["easy"])
			GUI.Label(new Rect(61, 304, 756, 107), "The galaxy is your playground and its people your\nplaythings. Enemies cower in your presence and\nplanets bow to your will. But is any of this for real...?", DifficultyLabel);
		else if (difficultyBool["normal"])
			GUI.Label(new Rect(61, 304, 756, 107), "Your foes are as numerous as they are deadly.\nEntire worlds stand against you. Many trials lie ahead,\nbut a firm resolve will give you the power to write history.", DifficultyLabel);
		else if (difficultyBool["hard"])
			GUI.Label(new Rect(61, 304, 756, 107), "A black wind blows across space as a massive army\nemerges from the depths of the abyss. This time, all the\nlights in the sky are your enemy. Are you prepared?", DifficultyLabel);
	}

	// Create OK Button
	private void CreateOKButton(){
		if (GUI.Button(new Rect(490, 427, 184, 47), "OK", OKButton) && guiAlpha == 1.0f){
			PlayerPrefs.SetString("difficulty", difficulty);
		}
	}

	// Create Cancel Button
	private void CreateCancelButton(){
		if (GUI.Button(new Rect(684, 427, 184, 47), "CANCEL") && guiAlpha == 1.0f){
			StartCoroutine(FadeOutGUI(1.0f, 0.0f, 0.5f, 0.0f));
			StartCoroutine(guiController.SwitchGUI("Title_GUI", 0.5f));
			cancelSelect.Play();
		}
	}
}