using UnityEngine;
using System.Collections;

public class MainMenuGUI : MonoBehaviour {
	bool mainMenu;
	bool levelSelectMenu;
	GameObject title;
	// Use this for initialization
	void Start ()
	{
		mainMenu = true;
		levelSelectMenu = false;
		title = GameObject.Find ("Title");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnGUI ()
	{
		if (GUI.Button(new Rect(20, 20, 50, 20), "Reset"))
		{
			PlayerPrefs.DeleteAll ();
		}

		if (mainMenu)
		{

			title.guiText.enabled = true;
			title.guiText.text = "LEGO MBA: The training begins";
		}
		else if (levelSelectMenu)
		{
			title.guiText.enabled = true;
			title.guiText.text = "Select level";
		}

		if (!PlayerPrefs.HasKey("levelsUnlocked"))
		{
			//New player
			//GUI.Box(new Rect(Screen.width/2-100, Screen.height/3+75, 200, 150), "N00b!");
			if (GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-50, 200, 100), "Start new game!"))
			{
				PlayerPrefs.SetInt ("levelsUnlocked", 1);
				//Load lvl 1
			}
		}

		else if (PlayerPrefs.HasKey("levelsUnlocked") && !levelSelectMenu)
		{
			if (GUI.Button(new Rect(Screen.width/2-100, Screen.height/2-50, 200, 100), "Continue!"))
			{
				//Go to last unlocked level
			}

			if (GUI.Button(new Rect(Screen.width/2-100, Screen.height/2+50, 200, 100), "Select level"))
			{
				levelSelectMenu = true;
			}
		
		}

		if (levelSelectMenu)
		{
			mainMenu = false;
			if (GUI.Button(new Rect(Screen.width/5-50, Screen.height/2-25, 100, 50), "Lvl 1"))
			{
				//Open lvl 1
			}
			if (GUI.Button(new Rect(Screen.width/5+50, Screen.height/2-25, 100, 50), "Lvl 2"))
			{
				//Open lvl 2
			}

			if (GUI.Button(new Rect((Screen.width*0.1f)-50, (Screen.height*0.8f)-25, 100, 50), "Back"))
			{
				levelSelectMenu = false;
				mainMenu = true;
			}
			
		}

	}
}
