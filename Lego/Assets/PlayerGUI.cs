using UnityEngine;
using System.Collections;

public class PlayerGUI : MonoBehaviour {
	bool pauseMenu;
	bool inventoryMenu;
	public bool narratorTalking;
	// Use this for initialization
	void Start () 
	{
		pauseMenu = false;
		inventoryMenu = false;
		narratorTalking = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		print ("PauseMenu = " + pauseMenu);
		if (Input.GetKeyDown ("e"))
		{
			if (pauseMenu == false && inventoryMenu == false)
			{
				inventoryMenu = true;
			}
			else if (inventoryMenu == true)
			{
				inventoryMenu = false;
			}
		}
		if (Input.GetKeyDown ("escape"))
		{
			if (pauseMenu == false && inventoryMenu == false)
			{
				pauseMenu = true;
			}
			else if (pauseMenu == true)
			{
				pauseMenu = false;
			}
		}

		if (pauseMenu || inventoryMenu)
		{
			Time.timeScale = 0;
			GameObject.Find ("First Person Controller").GetComponent<MouseLook>().enabled = false;
			GameObject.Find ("Main Camera").GetComponent<MouseLook>().enabled = false;


		} else
		{
			Time.timeScale = 1;
			GameObject.Find ("First Person Controller").GetComponent<MouseLook>().enabled = true;
			GameObject.Find ("Main Camera").GetComponent<MouseLook>().enabled = true;
		}
	}

	void OnGUI ()
	{
		if (!pauseMenu && !inventoryMenu)
		{
			//Draw crosshair and ghost brick
			//Draw selected brick and the number available

		}
		else if (pauseMenu)
		{
			if (GUI.Button(new Rect(Screen.width/2-50, Screen.height/5, 100, 50), "Return"))
			{
				pauseMenu = false;
			}
		}
		else if (inventoryMenu)
		{
			//Do some stuff to change selected block type


			if (GUI.Button(new Rect(Screen.width/2-50, Screen.height/5, 100, 50), "Return"))
			{
				inventoryMenu = false;
			}
		}
		if (narratorTalking)
		{
			//do stuff, then set narratorTalking to false again

		}
	}
}
