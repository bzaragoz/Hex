using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class Title_GUI : Hex_GUI {
	GUIContent[] resolutionList;
	private ComboBox resolutionControl = new ComboBox();
	GUIContent[] visualQualityList;
	private ComboBox visualQualityControl = new ComboBox();
	GUIContent[] interfaceSizeList;
	private ComboBox interfaceSizeControl = new ComboBox();
	GUIContent[] antiAliasingList;
	private ComboBox antiAliasingControl = new ComboBox();
	GUIContent[] textureQualityList;
	private ComboBox textureQualityControl = new ComboBox();
	GUIContent[] shadowQualityList;
	private ComboBox shadowQualityControl = new ComboBox();
	GUIContent[] shaderQualityList;
	private ComboBox shaderQualityControl = new ComboBox();

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
	private static GUIStyle MainMenuToggle;
	private static GUIStyle SaveLabel;
	private static GUIStyle GeneralToggle;
	private static GUIStyle GraphicsToggle;
	private static GUIStyle SoundToggle;
	private static GUIStyle ControlsToggle;
	private static GUIStyle SettingsWindow;
	private static GUIStyle DropDownButton;
	private static GUIStyle DropDownMenu;

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
	static Rect versionWindow = new Rect(742, 529, 165, 20); 
	static Rect loadGameWindow = new Rect(619, 30, 341, 542);
	static Rect settingsTabWindow = new Rect(619, 30, 50, 542);
	static Rect settingsWindow = new Rect(669, 30, 291, 542);
	static Rect exitWindow = new Rect((Screen.width/2)-(0.24792f*Screen.width/2), (Screen.height/2)-(0.17833f*Screen.height/2), 0.24792f*Screen.width, 0.17833f*Screen.height);

	// Window Booleans
	private Dictionary<string, bool> mainMenuBool = new Dictionary<string, bool>();
	private string mainMenuSelection = "NONE";
	private Dictionary<string, bool> settingsBool = new Dictionary<string, bool>();
	private string settingsSelection = "GENERAL";

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
	public float dialogVolume = 5.0F;
	public float uiVolume = 5.0F;
	public float qualityVolume = 5.0F;
	public float mouseSensitivity = 5.0F;

	private void Awake(){
		LoadMainMenu();
		LoadSettingsTabs();
		guiAlpha = 1.0f;

		resolutionList = new GUIContent[8];
		resolutionList[0] = new GUIContent("Window");
		resolutionList[1] = new GUIContent("800x600");
		resolutionList[2] = new GUIContent("1024x600");
		resolutionList[3] = new GUIContent("1024x768");
		resolutionList[4] = new GUIContent("1280x720");
		resolutionList[5] = new GUIContent("1280x768");
		resolutionList[6] = new GUIContent("1360x768");
		resolutionList[7] = new GUIContent("Fullscreen");
		resolutionControl.SetSelectedItemIndex(7);

		visualQualityList = new GUIContent[3];
		visualQualityList[0] = new GUIContent("Low");
		visualQualityList[1] = new GUIContent("Medium");
		visualQualityList[2] = new GUIContent("High");
		visualQualityControl.SetSelectedItemIndex(1);
		
		interfaceSizeList = new GUIContent[3];
		interfaceSizeList[0] = new GUIContent("Small");
		interfaceSizeList[1] = new GUIContent("Normal");
		interfaceSizeList[2] = new GUIContent("Large");
		interfaceSizeControl.SetSelectedItemIndex(1);

		antiAliasingList = new GUIContent[3];
		antiAliasingList[0] = new GUIContent("None");
		antiAliasingList[1] = new GUIContent("2X");
		antiAliasingList[2] = new GUIContent("4X");
		antiAliasingControl.SetSelectedItemIndex(1);

		textureQualityList = new GUIContent[3];
		textureQualityList[0] = new GUIContent("Low");
		textureQualityList[1] = new GUIContent("Medium");
		textureQualityList[2] = new GUIContent("High");
		textureQualityControl.SetSelectedItemIndex(1);

		shadowQualityList = new GUIContent[4];
		shadowQualityList[0] = new GUIContent("Off");
		shadowQualityList[1] = new GUIContent("Low");
		shadowQualityList[2] = new GUIContent("Medium");
		shadowQualityList[3] = new GUIContent("High");
		shadowQualityControl.SetSelectedItemIndex(2);

		shaderQualityList = new GUIContent[3];
		shaderQualityList[0] = new GUIContent("Low");
		shaderQualityList[1] = new GUIContent("Medium");
		shaderQualityList[2] = new GUIContent("High");
		shaderQualityControl.SetSelectedItemIndex(1);
	}

	// Load Main Menu
	private void LoadMainMenu(){
		mainMenuBool.Add("LOAD GAME", false);
		mainMenuBool.Add("SETTINGS", false);
		mainMenuBool.Add("EXIT", false);
	}
	
	// Load Settings
	private void LoadSettingsTabs(){
		settingsBool.Add("GENERAL", true);
		settingsBool.Add("GRAPHICS", false);
		settingsBool.Add("SOUND", false);
		settingsBool.Add("CONTROLS", false);
	}
	
	// Set Main Menu
	private void SetMainMenu(string mainMenuSelection){
		List<string> keys = new List<string>(mainMenuBool.Keys);
		
		foreach(string key in keys){
			if (key == mainMenuSelection){
				if (mainMenuBool[key] == false)
					mainMenuBool[key] = true;
				else
					mainMenuBool[key] = false;
			}
			else
				mainMenuBool[key] = false;
			this.mainMenuSelection = mainMenuSelection;
		}
	}
	
	// Set Settings
	private void SetSettingsTabs(string settingsSelection){
		List<string> keys = new List<string>(settingsBool.Keys);
		
		foreach(string key in keys){
			if (key == settingsSelection)
				settingsBool[key] = true;
			else
				settingsBool[key] = false;
			this.settingsSelection = settingsSelection;
		}
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
		MainMenuToggle = skin.GetStyle("MainMenuToggle");
		SaveLabel = skin.GetStyle("SaveLabel");
		GeneralToggle = skin.GetStyle("GeneralToggle");
		GraphicsToggle = skin.GetStyle("GraphicsToggle");
		SoundToggle = skin.GetStyle("SoundToggle");
		ControlsToggle = skin.GetStyle("ControlsToggle");
		SettingsWindow = skin.GetStyle("SettingsWindow");
		DropDownButton = skin.GetStyle("DropDownButton");
		DropDownMenu = skin.GetStyle("DropDownMenu");
	}

	// Load GUI
	protected override void LoadGUI(){
		CreateMatteBox(0.0f, 0.0f, 960.0f, 30.0f);
		CreateLogoWindow();
		LoadMainWindow();
		LoadVersionWindow();
		CreateMatteBox(0.0f, 572.0f, 960.0f, 30.0f);

		if (mainMenuBool["LOAD GAME"])
			LoadLoadWindow();
		if (mainMenuBool["SETTINGS"])
			LoadSettingsWindow();
		if (mainMenuBool["EXIT"])
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
		settingsTabWindow = GUI.Window (4, settingsTabWindow, CreateSettingsTabsWindow, "", SettingsTabWindow);
		settingsWindow = GUI.Window (5, settingsWindow, CreateSettingsWindow, "", SettingsWindow);	
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
			camController.ReplaceCamera("New_Game_Camera");
		}
		if (GUI.Button(new Rect(2, 117, 236, 31), "CREDITS", MainMenuButton)){
			print("Opening Credits...");
		}
		CreateMainMenuToggles();
	}

	// Create Main Menu Toggles
	private void CreateMainMenuToggles(){
		CreateMainMenuToggle(new Rect(2, 55, 236, 31), "LOAD GAME");
		CreateMainMenuToggle(new Rect(2, 86, 236, 31), "SETTINGS");
		CreateMainMenuToggle(new Rect(2, 148, 236, 31), "EXIT");
	}

	// Create Main Menu Toggle
	private void CreateMainMenuToggle(Rect newRect, string mainMenuSelection){
		if (GUI.Toggle(newRect, mainMenuBool[mainMenuSelection], mainMenuSelection, MainMenuToggle) != mainMenuBool[mainMenuSelection])
			SetMainMenu(mainMenuSelection);
	}
	
	// Create Version Window
	void CreateVersionWindow(int windowID){
		GUI.Label(new Rect(0, 0, 165, 20), "VERSION: PROTOTYPE", VersionLabel);
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
		if (loadGame != "none")
			GUI.Label(new Rect(21, 430, 307, 30), "Load " + loadGame + "?", LoadLabel);
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
		GUI.Label(new Rect (25, 96, 100, 20), "AUTO", SaveLabel);
		GUI.Label(new Rect (25, 199, 100, 20), "SAVE 1", SaveLabel);
		GUI.Label(new Rect (25, 303, 100, 20), "SAVE 2", SaveLabel);
		GUI.Label(new Rect (25, 406, 100, 20), "SAVE 3", SaveLabel);
	}

	// Create Load Button
	private void CreateLoadButton(){
		if (GUI.Button(new Rect(21, 470, 146, 51), "LOAD", LoadButton)){
			if (loadGame == "none")
				GUI.Label(new Rect(21, 430, 307, 30), "Save file not selected.", LoadLabel);
			else
				Application.LoadLevel("Main Menu");
		}
	}

	// Create Cancel Button
	private void CreateCancelButton(){
		if (GUI.Button(new Rect(182, 470, 146, 51), "CANCEL", LoadButton)){
			loadGame = "none";
			mainMenuBool["LOAD GAME"] = false;
		}
	}

	// Create Tabs Window
	void CreateSettingsTabsWindow(int windowID){
		CreateSettingsTabsToggles ();
	}
	
	// Create Main Menu Toggles
	private void CreateSettingsTabsToggles(){
		CreateSettingsTabsToggle(new Rect(0, 0, 50, 50), ref GeneralToggle, "GENERAL");
		CreateSettingsTabsToggle(new Rect(0, 50, 50, 50), ref GraphicsToggle, "GRAPHICS");
		CreateSettingsTabsToggle(new Rect(0, 100, 50, 50), ref SoundToggle, "SOUND");
		CreateSettingsTabsToggle(new Rect(0, 150, 50, 50), ref ControlsToggle, "CONTROLS");
	}
	
	// Create Settings Toggle
	private void CreateSettingsTabsToggle(Rect newRect, ref GUIStyle settingsToggle, string settingsSelection){
		if (GUI.Toggle(newRect, settingsBool[settingsSelection], "", settingsToggle) != settingsBool[settingsSelection])
			SetSettingsTabs(settingsSelection);
	}

	// Create Settings Window
	void CreateSettingsWindow(int windowID){
		if (settingsBool["GENERAL"])
			GeneralItems ();
		else if (settingsBool["GRAPHICS"])
			GraphicsItems();
		else if (settingsBool["SOUND"])
			SoundItems();
		else if (settingsBool["CONTROLS"])
			ControlsItems();
		GUI.Button(new Rect(10, 431, 131, 39), "DEFAULT", LoadButton);
		GUI.Button(new Rect(150, 431, 131, 39), "RESET", LoadButton);
		if (GUI.Button(new Rect(10, 482, 131, 39), "APPLY", LoadButton)){ //493
			mainMenuBool["SETTINGS"] = false;
		}
		if (GUI.Button(new Rect(150, 482, 131, 39), "CANCEL", LoadButton)){ //493
			settingsTab = "general";
			mainMenuBool["SETTINGS"] = false;
		}
	}

	// General Settings
	void GeneralItems(){
		GUI.Label(new Rect(10, 10, 153, 25), "GENERAL", HeaderLabel);
	}

	// Graphics Settings
	void GraphicsItems(){
		GUI.Label(new Rect(10, 10, 153, 25), "GRAPHICS", HeaderLabel);
		GUI.Label(new Rect(26, 49, 120, 20), "DISPLAY");
		GUI.Label(new Rect(42, 78, 120, 20), "Resolution");
		GUI.Label(new Rect(42, 118, 120, 20), "Visual Quality");
		GUI.Label(new Rect(42, 158, 120, 20), "Interface Size");
		GUI.Label(new Rect(42, 198, 120, 20), "Anti-Aliasing");
		GUI.Label(new Rect(26, 257, 120, 20), "ADVANCED");
		GUI.Label(new Rect(42, 286, 120, 20), "Texture Quality");
		GUI.Label(new Rect(42, 326, 120, 20), "Shadow Quality");
		GUI.Label(new Rect(42, 366, 120, 20), "Shader Quality");

		int shaderQualityIndex = shaderQualityControl.GetSelectedItemIndex();
		shaderQualityIndex = shaderQualityControl.List(new Rect(58, 386, 200, 20), shaderQualityList[shaderQualityIndex].text, shaderQualityList, DropDownButton, DropDownMenu, DropDownMenu);
		int shadowQualityIndex = shadowQualityControl.GetSelectedItemIndex();
		shadowQualityIndex = shadowQualityControl.List(new Rect(58, 346, 200, 20), shadowQualityList[shadowQualityIndex].text, shadowQualityList, DropDownButton, DropDownMenu, DropDownMenu);
		int textureQualityIndex = textureQualityControl.GetSelectedItemIndex();
		textureQualityIndex = textureQualityControl.List(new Rect(58, 306, 200, 20), textureQualityList[textureQualityIndex].text, textureQualityList, DropDownButton, DropDownMenu, DropDownMenu);
		int antiAliasingIndex = antiAliasingControl.GetSelectedItemIndex();
		antiAliasingIndex = antiAliasingControl.List(new Rect(58, 218, 200, 20), antiAliasingList[antiAliasingIndex].text, antiAliasingList, DropDownButton, DropDownMenu, DropDownMenu);
		int interfaceSizeIndex = interfaceSizeControl.GetSelectedItemIndex();
		interfaceSizeIndex = interfaceSizeControl.List(new Rect(58, 178, 200, 20), interfaceSizeList[interfaceSizeIndex].text, interfaceSizeList, DropDownButton, DropDownMenu, DropDownMenu);
		int visualQualityIndex = visualQualityControl.GetSelectedItemIndex();
		visualQualityIndex = visualQualityControl.List(new Rect(58, 138, 200, 20), visualQualityList[visualQualityIndex].text, visualQualityList, DropDownButton, DropDownMenu, DropDownMenu);
		int resolutionIndex = resolutionControl.GetSelectedItemIndex();
		resolutionIndex = resolutionControl.List(new Rect(58, 98, 200, 20), resolutionList[resolutionIndex].text, resolutionList, DropDownButton, DropDownMenu, DropDownMenu);
	}

	// Sound Settings
	void SoundItems(){
		GUI.Label(new Rect(10, 10, 153, 25), "SOUND", HeaderLabel);
		GUI.Label(new Rect(26, 49, 120, 20), "QUALITY");
		qualityVolume = GUI.HorizontalSlider(new Rect(58, 78, 200, 20), qualityVolume, 0.0F, 10.0F);
		GUI.Label(new Rect(26, 117, 120, 20), "VOLUME");
		GUI.Label(new Rect(42, 146, 120, 20), "Master Volume");
		masterVolume = GUI.HorizontalSlider(new Rect(58, 166, 200, 20), masterVolume, 0.0F, 10.0F);
		GUI.Label(new Rect(42, 186, 120, 20), "Background");
		backgroundVolume = GUI.HorizontalSlider(new Rect(58, 206, 200, 20), backgroundVolume, 0.0F, 10.0F);
		GUI.Label(new Rect(42, 226, 120, 20), "Effects");
		effectsVolume = GUI.HorizontalSlider(new Rect(58, 246, 200, 20), effectsVolume, 0.0F, 10.0F);
		GUI.Label(new Rect(42, 266, 120, 20), "Dialog");
		dialogVolume = GUI.HorizontalSlider(new Rect(58, 286, 200, 20), dialogVolume, 0.0F, 10.0F);
		GUI.Label(new Rect(42, 306, 120, 20), "UI");
		uiVolume = GUI.HorizontalSlider(new Rect(58, 326, 200, 20), uiVolume, 0.0F, 10.0F);
	}

	// Control Settings
	void ControlsItems(){
		GUI.Label(new Rect(10, 10, 153, 25), "CONTROLS", HeaderLabel);
		GUI.Label(new Rect(26, 49, 120, 20), "MOUSE");
		GUI.Label(new Rect(42, 78, 120, 20), "Sensitivity");
		mouseSensitivity = GUI.HorizontalSlider(new Rect(58, 98, 200, 20), mouseSensitivity, 0.0F, 10.0F);
	}

	// Exit
	void ExitItems(int windowID){
		GUI.Label(new Rect(0,0, 200, 25), "Are you sure you want to exit?");
		if (GUI.Button (new Rect(0.01146f*Screen.width, 0.105f*Screen.height, 0.10625f*Screen.width, 0.05667f*Screen.height), "OK")) {
			Application.Quit();
		}
		if (GUI.Button(new Rect(0.13021f*Screen.width, 0.105f*Screen.height, 0.10625f*Screen.width, 0.05667f*Screen.height), "CANCEL")){
			mainMenuBool["EXIT"] = false;
		}
	}
}