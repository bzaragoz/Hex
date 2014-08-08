using UnityEngine;
using System.Collections;
using System.Xml;
using System;

public class Title_GUI : Hex_GUI {

	// GUI Styles
	private static GUIStyle MainMenuButton;
	private static GUIStyle LoadSaveButton;
	private static GUIStyle VersionLabel;
	private static GUIStyle LoadLabel;
	private static GUIStyle MainMenuWindow;
	private static GUIStyle MatteBox;
	private static GUIStyle LogoBox;
	private static GUIStyle LoadWindow;
	private static GUIStyle HeaderLabel;
	private static GUIStyle SettingsTabWindow;
	private static GUIStyle LoadButton;

	// Load-Window Textures
	private static Texture2D autosaveTexture = (Texture2D)Resources.Load ("Textures/autosave");
	private static Texture2D save1Texture = (Texture2D)Resources.Load ("Textures/save1");
	private static Texture2D save2Texture = (Texture2D)Resources.Load ("Textures/save2");
	private static Texture2D save3Texture = (Texture2D)Resources.Load ("Textures/save3");
	// Settings Window Textures
	private static Texture generalTexture = (Texture)Resources.Load("Textures/general");
	private static Texture graphicsTexture = (Texture)Resources.Load("Textures/graphics");
	private static Texture soundTexture = (Texture)Resources.Load("Textures/sound");
	private static Texture controlsTexture = (Texture)Resources.Load("Textures/controls");
	
	// Window Rectangles
	static Rect logoWindow = new Rect(702, 56, 258, 83);
	static Rect mainMenuWindow = new Rect(40, 30, 238, 184);
	static Rect versionWindow = new Rect(742, 529, 165, 10); 
	static Rect loadGameWindow = new Rect(619, 30, 341, 542);
	static Rect settingsTabWindow = new Rect(619, 30, 50, 200);
	static Rect settingsWindow = new Rect(669, 30, 291, 542);
	static Rect exitWindow = new Rect((Screen.width/2)-(0.24792f*Screen.width/2), (Screen.height/2)-(0.17833f*Screen.height/2), 0.24792f*Screen.width, 0.17833f*Screen.height);

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

	private void Awake(){
		guiAlpha = 1.0f;
	}

	// Load Skin
	protected override void LoadSkin(){
		skin = (GUISkin)Resources.Load("Skins/TitleSkin");
	}
	
	// Load Styles
	protected override void LoadStyles(){
		MainMenuButton = skin.GetStyle("MainMenuButton");
		LoadSaveButton = skin.GetStyle("LoadSaveButton");
		VersionLabel = skin.GetStyle("VersionLabel");
		LoadLabel = skin.GetStyle("LoadLabel");
		MainMenuWindow = skin.GetStyle("MainMenuWindow");
		MatteBox = skin.GetStyle("MatteBox");
		LogoBox = skin.GetStyle("LogoBox");
		LoadWindow = skin.GetStyle("LoadWindow");
		HeaderLabel = skin.GetStyle ("HeaderLabel");
		SettingsTabWindow = skin.GetStyle ("SettingsTabWindow");
		LoadButton = skin.GetStyle ("LoadButton");
	}

	// Load GUI
	protected override void LoadGUI(){
		CreateMatteBox(0.0f, 0.0f, 960.0f, 30.0f);
		CreateLogoWindow();
		LoadMainWindow();
		LoadVersionWindow();
		CreateMatteBox(0.0f, 572.0f, 960.0f, 30.0f);

		if (load)
			LoadLoadWindow();
		if (settings)
			LoadSettingsWindow();
		if (exit)
			LoadExitWindow();
	}

	// Load Main Menu Window
	private void LoadMainWindow(){
		mainMenuWindow = GUI.Window(1, mainMenuWindow, CreateMainWindow, "", MainMenuWindow);
	}

	// Load Version Window
	private void LoadVersionWindow(){
		versionWindow = GUI.Window (2, versionWindow, CreateVersionWindow, "");
	}

	// Load Load Window
	private void LoadLoadWindow(){
		loadGameWindow = GUI.Window(3, loadGameWindow, CreateLoadWindow, "", LoadWindow);	
	}

	// Load Settings Window
	private void LoadSettingsWindow(){
		settingsTabWindow = GUI.Window (4, settingsTabWindow, CreateTabsWindow, "", SettingsTabWindow);
		settingsWindow = GUI.Window (5, settingsWindow, CreateSettingsWindow, "");	
	}

	// Load Exit Window
	private void LoadExitWindow(){
		exitWindow = GUI.Window (6, exitWindow, ExitItems, "");
	}

	// Create Matte Box
	private void CreateMatteBox(float left, float top, float width, float height){
		GUI.Box(new Rect(left, top, width, height), "", MatteBox);
	}

	// Create Logo Window
	private void CreateLogoWindow(){
		logoWindow = GUI.Window(0, logoWindow, NoItems, "", LogoBox);
	}

	// Create Main Window
	void CreateMainWindow(int windowID){
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

	// Create Version Window
	void CreateVersionWindow(int windowID){
		GUI.Label(new Rect(0, 0, 0.17188f*Screen.width, 0.03667f*Screen.height), "Version: Prototype", VersionLabel);
	}

	// Create Load Window
	void CreateLoadWindow(int windowID){
		CreateSaveButton(ref autosaveButton,
		                 new Rect(21, 18, 307, 95),
		                 new GUIContent("  Jonathan\n  NORMAL\n  The Gatehouse\n  Progress: 3%", autosaveTexture),
		                 ref LoadSaveButton, "Autosave", "Main Menu");
		CreateSaveButton(ref save1Button,
		                 new Rect(21, 121, 307, 95),
		                 new GUIContent("  Joseph\n  NORMAL\n  Garden Labyrinth\n  Progress: 6%", save1Texture),
		                 ref LoadSaveButton, "Save 1", "Main Menu");
		CreateSaveButton(ref save2Button,
		                 new Rect(21, 225, 307, 95),
		                 new GUIContent("  Jotaro\n  NORMAL\n  Castle Courtyard\n  Progress: 8%", save2Texture),
		                 ref LoadSaveButton, "Save 2", "Main Menu");
		CreateSaveButton(ref save3Button,
		                 new Rect(21, 328, 307, 95),
		                 new GUIContent("  Josuke\n  NORMAL\n  Great Keep\n  Progress: 11%", save3Texture),
		                 ref LoadSaveButton, "Save 3", "Main Menu");

		CreateLabels();
		CreateLoadButton();
		CreateCancelButton();
	}

	// Create Save Button
	private void CreateSaveButton(ref AdvancedButton saveButton,
	                              Rect newRect, GUIContent newContent, ref GUIStyle newStyle,
	                              string loadGame, string level){
		AdvancedButtonResult saveResult = saveButton.Draw(newRect, newContent, newStyle);

		if (saveResult == AdvancedButtonResult.SimpleClick)
			this.loadGame = loadGame;
		else if (saveResult == AdvancedButtonResult.DoubleClick){
			this.loadGame = "none";
			Application.LoadLevel(level);
		}
	}

	// Create Save Labels
	private void CreateLabels(){
		GUI.Label(new Rect (25, 96, 100, 20), "AUTO");
		GUI.Label(new Rect (25, 199, 100, 20), "SAVE 1");
		GUI.Label(new Rect (25, 303, 100, 20), "SAVE 2");
		GUI.Label(new Rect (25, 406, 100, 20), "SAVE 3");
	}

	// Create Load Button
	private void CreateLoadButton(){
		if (loadGame != "none"){
			GUI.Label(new Rect(21, 420, 307, 30), "Do you want to load " + loadGame + "?", LoadLabel);
			if (GUI.Button(new Rect(21, 470, 146, 51), "LOAD", LoadButton)){
				if (loadGame == "none")
					print("No save file selected.");
				else
					Application.LoadLevel("Main Menu");
			}
		}		
	}

	// Create Cancel Button
	private void CreateCancelButton(){
		if (GUI.Button(new Rect(182, 470, 146, 51), "CANCEL")){
			loadGame = "none";
			load = false;
		}
	}

	// Create Tabs Window
	void CreateTabsWindow(int windowID){
		if (GUI.Button(new Rect(0, 0, 50, 50), generalTexture))
			settingsTab = "general";
		if (GUI.Button (new Rect (0, 50, 50, 50), graphicsTexture))
			settingsTab = "graphics";
		if (GUI.Button(new Rect(0, 100, 50, 50), soundTexture))
			settingsTab = "sound";
		if (GUI.Button(new Rect(0, 150, 50, 50), controlsTexture))
			settingsTab = "controls";
	}

	// Create Settings Window
	void CreateSettingsWindow(int windowID){
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
		GUI.Label(new Rect(0.01042f*Screen.width, 0.01667f*Screen.height, 0.12708f*Screen.width, 0.04667f*Screen.height), "GENERAL", HeaderLabel);
	}

	// Graphics Settings
	void GraphicsItems(){
		GUI.Label(new Rect(0.01042f*Screen.width, 0.01667f*Screen.height, 0.12708f*Screen.width, 0.04667f*Screen.height), "GRAPHICS", HeaderLabel);
		GUI.Label(new Rect(0.02813f*Screen.width, 0.08833f*Screen.height, 0.08125f*Screen.width, 0.035f*Screen.height), "Display");
		GUI.Label(new Rect(0.04583f*Screen.width, 0.14833f*Screen.height, 0.06354f*Screen.width, 0.035f*Screen.height), "Resolution");
		GUI.Label(new Rect(0.04583f*Screen.width, 0.20833f*Screen.height, 0.06354f*Screen.width, 0.035f*Screen.height), "Quality");
	}

	// Sound Settings
	void SoundItems(){
		GUI.Label(new Rect(0.01042f*Screen.width, 0.01667f*Screen.height, 0.12708f*Screen.width, 0.04667f*Screen.height), "SOUND", HeaderLabel);
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
		GUI.Label(new Rect(0.01042f*Screen.width, 0.01667f*Screen.height, 0.12708f*Screen.width, 0.04667f*Screen.height), "CONTROLS", HeaderLabel);
		GUI.Label(new Rect(0.02813f*Screen.width, 0.08833f*Screen.height, 0.08125f*Screen.width, 0.035f*Screen.height), "Mouse");
		GUI.Label(new Rect(0.04583f*Screen.width, 0.14833f*Screen.height, 0.06354f*Screen.width, 0.035f*Screen.height), "Sensitivity");
		mouseSensitivity = GUI.HorizontalSlider(new Rect(0.11667f*Screen.width, 0.14833f*Screen.height, 0.17188f*Screen.width, 0.035f*Screen.height), mouseSensitivity, 0.0F, 10.0F);
	}

	// Exit
	void ExitItems(int windowID){
		GUI.Label(new Rect(0,0, 200, 25), "Are you sure you want to exit?");
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