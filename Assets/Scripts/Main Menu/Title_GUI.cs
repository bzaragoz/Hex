using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class Title_GUI : MonoBehaviour {

	float originalWidth = 960;
	float originalHeight = 600;

	static float mainMenu_left = 0.04167f*Screen.width;
	static float mainMenu_top = 0.05f*Screen.height;
	static float mainMenu_width = 0.24792f*Screen.width;
	static float mainMenu_height = 0.30667f*Screen.height;
	static float button_height = 0.15217f*mainMenu_height;

	// GUI Controller
	GUI_Controller guiController;

	// GUI Skin and Styles
	public GUISkin TitleSkin = (GUISkin)Resources.Load("Skins/TitleSkin");
	public GUIStyle MainMenuButton;
	public GUIStyle LoadGameButton;
	public GUIStyle VersionLabel;
	public GUIStyle LoadLabel;
	public GUIStyle MainMenuWindow;
	public GUIStyle MatteBox;
	public GUIStyle Logo;

	// Load Window Textures
	public Texture2D autosaveTexture = (Texture2D)Resources.Load ("Textures/save");
	public Texture2D save1Texture = (Texture2D)Resources.Load ("Textures/save");
	public Texture2D save2Texture = (Texture2D)Resources.Load ("Textures/save");
	public Texture2D save3Texture = (Texture2D)Resources.Load ("Textures/save");

	// Settings Window Textures
	public Texture generalTexture = (Texture)Resources.Load("Textures/general");
	public Texture graphicsTexture = (Texture)Resources.Load("Textures/graphics");
	public Texture soundTexture = (Texture)Resources.Load("Textures/sound");
	public Texture controlsTexture = (Texture)Resources.Load("Textures/controls");
	
	// Read .xml
	XmlDocument xmlDoc = new XmlDocument();
	//xmlDoc.LoadXml();

/*
	XmlElement node = xmlDoc.SelectNodes("logo");
	float logo_left = float.Parse(node.SelectSingleNode("left").InnerText)*Screen.width;
	float logo_top = float.Parse(node.SelectSingleNode("top").InnerText)*Screen.width;
	float logo_width = float.Parse(node.SelectSingleNode("width").InnerText)*Screen.width;
	float logo_height = float.Parse(node.SelectSingleNode("height").InnerText)*Screen.width;
	float button_height = 0.15217f*mainMenu_height;

	XmlElement node2 = xmlDoc.SelectNodes("main menu");
	float mainMenu_left = float.Parse(node.SelectSingleNode("left").InnerText)*Screen.width;
	float mainMenu_top = float.Parse(node.SelectSingleNode("top").InnerText)*Screen.width;
	float mainMenu_width = float.Parse(node.SelectSingleNode("width").InnerText)*Screen.width;
	float mainMenu_height = float.Parse(node.SelectSingleNode("height").InnerText)*Screen.width;
*/

	// Window Rectangles
	Rect logoWindow = new Rect(702, 56, 258, 83);
	Rect mainMenuWindow = new Rect(40, 30, 238, 184);
	Rect versionWindow = new Rect(742, 529, 165, 10); 
	Rect loadGameWindow = new Rect(619, 30, 341, 540);
	Rect settingsTabWindow = new Rect(619, 30, 50, 200);
	Rect settingsWindow = new Rect(669, 30, 291, 542);
	Rect exitWindow = new Rect((Screen.width/2)-(0.24792f*Screen.width/2), (Screen.height/2)-(0.17833f*Screen.height/2), 0.24792f*Screen.width, 0.17833f*Screen.height);

	// Window Booleans
	bool load = false;
	bool settings = false;
	bool exit = false;

	// String Variables
	string loadGame = "none";
	string settingsTab = "general";

	// GUI Items
	private AdvancedButton autosaveButton = new AdvancedButton( 0.1f, 0.3f, 1f );
	private AdvancedButton save1Button = new AdvancedButton( 0.1f, 0.3f, 1f );
	private AdvancedButton save2Button = new AdvancedButton( 0.1f, 0.3f, 1f );
	private AdvancedButton save3Button = new AdvancedButton( 0.1f, 0.3f, 1f );

	public float masterVolume = 5.0F;
	public float backgroundVolume = 5.0F;
	public float effectsVolume = 5.0F;
	public float mouseSensitivity = 5.0F;

	// Run on Start
	void Start(){
		guiController = GameObject.Find ("GUI").GetComponent<GUI_Controller>();
	}

	void Update(){
	}

	// Run on GUI
	void OnGUI () {
		GUI.skin = TitleSkin;

		// Set matrix
		Vector2 ratio = new Vector2(Screen.width/originalWidth , Screen.height/originalHeight );
		Matrix4x4 guiMatrix = Matrix4x4.identity;
		guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
		GUI.matrix = guiMatrix;

		MainMenuButton = new GUIStyle(GUI.skin.GetStyle("MainMenuButton"));
		LoadGameButton = new GUIStyle(GUI.skin.GetStyle("LoadGameButton"));
		VersionLabel = new GUIStyle(GUI.skin.GetStyle("VersionLabel"));
		LoadLabel = new GUIStyle(GUI.skin.GetStyle("LoadLabel"));
		MainMenuWindow = new GUIStyle(GUI.skin.GetStyle ("MainMenuWindow"));
		MatteBox = new GUIStyle(GUI.skin.GetStyle ("MatteBox"));
		Logo = new GUIStyle(GUI.skin.GetStyle ("logo"));

		//MainMenuButton.fontSize = (int)(0.15217f*mainMenu_height)+20;
		LoadGameButton.fontSize = (int)(0.15833f*Screen.height)/6;
		VersionLabel.fontSize = (int)(0.03667f*Screen.height)+8;

		GUI.Box(new Rect (0, 0, 960, 30), "", MatteBox);
		logoWindow = GUI.Window(0, logoWindow, LogoItems, "", Logo);
		mainMenuWindow = GUI.Window(1, mainMenuWindow, MainMenuItems, "", MainMenuWindow);
		versionWindow = GUI.Window (2, versionWindow, versionItems, "");
		GUI.Box(new Rect (0, 572, 960, 30), "", MatteBox);
		if (load)
			loadGameWindow = GUI.Window(3, loadGameWindow, LoadGameItems, "");
		if (settings){
			settingsTabWindow = GUI.Window (4, settingsTabWindow, SettingsTabItems, "");
			settingsWindow = GUI.Window (5, settingsWindow, SettingsItems, "");
		}
		if (exit)
			exitWindow = GUI.Window (6, exitWindow, ExitItems, "");

		// Reset matrix
		GUI.matrix = Matrix4x4.identity;
	}
	// Logo Window
	void LogoItems(int windowID){
		
	}

	// Main Menu
	void MainMenuItems(int windowID){
		if (GUI.Button(new Rect(2, 24, 236, 31), "NEW GAME", MainMenuButton) || Input.GetKeyDown(KeyCode.N)){
			guiController.ReplaceGUI("New_Game_GUI");
		}
		if (GUI.Button(new Rect(2, 55, 236, 31), "LOAD GAME", MainMenuButton) || Input.GetKeyDown(KeyCode.L)){
			resetWindows();
			load = true;
		}
		if (GUI.Button(new Rect(2, 86, 236, 31), "SETTINGS", MainMenuButton) || Input.GetKeyDown(KeyCode.S)){
			resetWindows();
			settings = true;
		}
		if (GUI.Button(new Rect(2, 117, 236, 31), "CREDITS", MainMenuButton)){
			print("Opening Credits...");
		}
		if (GUI.Button(new Rect(2, 148, 236, 31), "EXIT", MainMenuButton) || Input.GetKeyDown(KeyCode.E)){
			resetWindows();
			exit = true;
		}
	}

	// Version Window
	void versionItems(int windowID){
		GUI.Label(new Rect(0, 0, 0.17188f*Screen.width, 0.03667f*Screen.height), "Version: Prototype", VersionLabel);
	}

	// Load Game
	void LoadGameItems(int windowID){
		AdvancedButtonResult autosaveResult = autosaveButton.Draw(new Rect(17, 18, 307, 95), new GUIContent("Autosave", autosaveTexture), LoadGameButton);
		if (autosaveResult == AdvancedButtonResult.SimpleClick) {
			loadGame = "Autosave";
		}
		else if (autosaveResult == AdvancedButtonResult.DoubleClick) {
			loadGame = "none";
			Application.LoadLevel ("Main Menu");
		}
		AdvancedButtonResult save1Result = save1Button.Draw(new Rect(17, 121, 307, 95), new GUIContent("Save 1", save1Texture), LoadGameButton);
		if (save1Result == AdvancedButtonResult.SimpleClick){
			loadGame = "Save 1";
		}
		else if (save1Result == AdvancedButtonResult.DoubleClick) {
			loadGame = "none";
			Application.LoadLevel ("Main Menu");
		}
		AdvancedButtonResult save2Result = save2Button.Draw(new Rect(17, 225, 307, 95), new GUIContent("Save 2", save2Texture), LoadGameButton);
		if (save2Result == AdvancedButtonResult.SimpleClick){
			loadGame = "Save 2";
		}
		else if (save2Result == AdvancedButtonResult.DoubleClick) {
			loadGame = "none";
			Application.LoadLevel ("Main Menu");
		}
		AdvancedButtonResult save3Result = save3Button.Draw(new Rect(17, 328, 307, 95), new GUIContent("Save 3", save3Texture), LoadGameButton);
		if (save3Result == AdvancedButtonResult.SimpleClick){
			loadGame = "Save 3";
		}
		else if (save3Result == AdvancedButtonResult.DoubleClick) {
			loadGame = "none";
			Application.LoadLevel ("Main Menu");
		}
		if (loadGame != "none"){
			GUI.Label(new Rect(17, 432, 307, 30), "Do you want to load " + loadGame + "?", LoadLabel);
			if (GUI.Button(new Rect(17, 470, 146, 51), "LOAD")){
				if (loadGame == "none")
					print("No save file selected.");
				else
					Application.LoadLevel("Main Menu");
			}
			if (GUI.Button(new Rect(178, 470, 146, 51), "CANCEL")){
				loadGame = "none";
				load = false;
			}
		}
	}

	// Settings Tabs
	void SettingsTabItems(int windowID){
		if (GUI.Button(new Rect(0, 0, 50, 50), generalTexture))
			settingsTab = "general";
		if (GUI.Button (new Rect (0, 50, 50, 50), graphicsTexture))
			settingsTab = "graphics";
		if (GUI.Button(new Rect(0, 100, 50, 50), soundTexture))
			settingsTab = "sound";
		if (GUI.Button(new Rect(0, 150, 50, 50), controlsTexture))
			settingsTab = "controls";
	}

	// Settings
	void SettingsItems(int windowID){
		if (settingsTab == "general")
			GeneralItems ();
		else if (settingsTab == "graphics")
			GraphicsItems();
		else if (settingsTab == "sound")
			SoundItems();
		else if (settingsTab == "controls")
			ControlsItems();
		if (GUI.Button(new Rect(10, 493, 131, 39), "APPLY")){
			settings = false;
		}
		if (GUI.Button(new Rect(150, 493, 131, 39), "CANCEL")){
			settingsTab = "general";
			settings = false;
		}
	}

	// General Settings
	void GeneralItems(){
		GUI.Label(new Rect(0.01042f*Screen.width, 0.01667f*Screen.height, 0.12708f*Screen.width, 0.04667f*Screen.height), "GENERAL");
	}

	// Graphics Settings
	void GraphicsItems(){
		GUI.Label(new Rect(0.01042f*Screen.width, 0.01667f*Screen.height, 0.12708f*Screen.width, 0.04667f*Screen.height), "GRAPHICS");
		GUI.Label(new Rect(0.02813f*Screen.width, 0.08833f*Screen.height, 0.08125f*Screen.width, 0.035f*Screen.height), "Display");
		GUI.Label(new Rect(0.04583f*Screen.width, 0.14833f*Screen.height, 0.06354f*Screen.width, 0.035f*Screen.height), "Resolution");
		GUI.Label(new Rect(0.04583f*Screen.width, 0.20833f*Screen.height, 0.06354f*Screen.width, 0.035f*Screen.height), "Quality");
	}

	// Sound Settings
	void SoundItems(){
		GUI.Label(new Rect(0.01042f*Screen.width, 0.01667f*Screen.height, 0.12708f*Screen.width, 0.04667f*Screen.height), "SOUND");
		GUI.Label(new Rect(0.02813f*Screen.width, 0.08833f*Screen.height, 0.08125f*Screen.width, 0.035f*Screen.height), "Volume");
		GUI.Label(new Rect(0.04583f*Screen.width, 0.14833f*Screen.height, 0.06354f*Screen.width, 0.035f*Screen.height), "Master");
		masterVolume = GUI.HorizontalSlider(new Rect(0.11667f*Screen.width, 0.14833f*Screen.height, 0.17188f*Screen.width, 0.035f*Screen.height), masterVolume, 0.0F, 10.0F);
		GUI.Label(new Rect(0.04583f*Screen.width, 0.20833f*Screen.height, 0.06354f*Screen.width, 0.035f*Screen.height), "Background");
		backgroundVolume = GUI.HorizontalSlider(new Rect(0.11667f*Screen.width, 0.20833f*Screen.height, 0.17188f*Screen.width, 0.035f*Screen.height), backgroundVolume, 0.0F, 10.0F);
		GUI.Label(new Rect(0.04583f*Screen.width, 0.26833f*Screen.height, 0.06354f*Screen.width, 0.035f*Screen.height), "Effects");
		effectsVolume = GUI.HorizontalSlider(new Rect(0.11667f*Screen.width, 0.26833f*Screen.height, 0.17188f*Screen.width, 0.035f*Screen.height), effectsVolume, 0.0F, 10.0F);
	}

	// Control Settings
	void ControlsItems(){
		GUI.Label(new Rect(0.01042f*Screen.width, 0.01667f*Screen.height, 0.12708f*Screen.width, 0.04667f*Screen.height), "CONTROLS");
		GUI.Label(new Rect(0.02813f*Screen.width, 0.08833f*Screen.height, 0.08125f*Screen.width, 0.035f*Screen.height), "Mouse");
		GUI.Label(new Rect(0.04583f*Screen.width, 0.14833f*Screen.height, 0.06354f*Screen.width, 0.035f*Screen.height), "Sensitivity");
		mouseSensitivity = GUI.HorizontalSlider(new Rect(0.11667f*Screen.width, 0.14833f*Screen.height, 0.17188f*Screen.width, 0.035f*Screen.height), mouseSensitivity, 0.0F, 10.0F);
	}

	// Exit
	void ExitItems(int windowID){
		GUI.Label(new Rect(0,0, 200, button_height), "Are you sure you want to exit?");
		if (GUI.Button (new Rect(0.01146f*Screen.width, 0.105f*Screen.height, 0.10625f*Screen.width, 0.05667f*Screen.height), "OK")) {
			Application.Quit();
		}
		if (GUI.Button(new Rect(0.13021f*Screen.width, 0.105f*Screen.height, 0.10625f*Screen.width, 0.05667f*Screen.height), "CANCEL")){
			exit = false;
		}
	}

	// Reset Window Booleans
	void resetWindows(){
		load = false;
		settings = false;
		exit = false;
	}
}