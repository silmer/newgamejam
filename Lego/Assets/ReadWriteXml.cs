using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;


public class ReadWriteXml : MonoBehaviour // the Class.
{

	public bool showSaveMenu;
	private Rect windowRect = new Rect((Screen.width/2)-50, 10, 100, 50);
	

	void OnGUI()
	{
		windowRect = GUILayout.Window(0, windowRect, UpdateWindow, "");
		
		GUILayout.BeginArea(new Rect(Screen.width/2-50,10,100,50));
		GUILayout.EndArea();
	}
	
	void UpdateWindow(int windowID)
	{
	
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Save"))
		{
			WriteToXml(); // calls the function when the button is pressed.
		}
		GUILayout.FlexibleSpace ();
		if(GUILayout.Button("Load"))
		{
			LoadFromXml(); // calls the function when the button is pressed.
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		
	}
	
	public void WriteToXml()
	{
		
		string filepath = Application.dataPath + @"/Data/gamexmldata.xml";

		XmlDocument xmlDoc = new XmlDocument();


		if (!File.Exists (filepath)) {
			XmlElement l = xmlDoc.CreateElement ("level");
			xmlDoc.AppendChild(l);
		} else {
			xmlDoc.Load (filepath);
		}


		XmlElement eRoot = xmlDoc.DocumentElement;

		eRoot.RemoveAll (); // remove all inside the transforms node.

		GameObject[] objects = GameObject.FindGameObjectsWithTag ("Saveable");
		foreach (GameObject obj in objects) {
			if (obj.CompareTag("Saveable")) {
				XmlElement e = xmlDoc.CreateElement ("GameObject");

				XmlElement type = xmlDoc.CreateElement("type");
				type.InnerText = obj.name;
				e.AppendChild(type);
				XmlElement pos = xmlDoc.CreateElement("position");

				XmlElement posx = xmlDoc.CreateElement("x");
				posx.InnerText = obj.transform.GetChild(0).transform.position.x.ToString();
				pos.AppendChild(posx);
				XmlElement posy = xmlDoc.CreateElement("y");
				posy.InnerText = obj.transform.GetChild(0).position.y.ToString();
				pos.AppendChild(posy);
				XmlElement posz = xmlDoc.CreateElement("z");
				posz.InnerText = obj.transform.GetChild(0).position.z.ToString();
				pos.AppendChild(posz);
				e.AppendChild(pos);



				eRoot.AppendChild(e);
			}
		}
		xmlDoc.Save (filepath); // save file.
	}
	
	public void LoadFromXml()
	{
				string filepath = Application.dataPath + @"/Data/gamexmldata.xml";
				XmlDocument xmlDoc = new XmlDocument ();
				GameObject[] objects = GameObject.FindGameObjectsWithTag("Saveable") as GameObject[];
				foreach (GameObject obj in objects) {
					Destroy(obj);	
				}
				if (File.Exists (filepath)) {
					xmlDoc.Load (filepath);

					XmlNodeList gameObjectList = xmlDoc.FirstChild.ChildNodes;

					foreach (XmlNode gameObject in gameObjectList) {
						float x = 0;
						float y = 0;
						float z = 0;
						string type = "";
						XmlNodeList objContent = gameObject.ChildNodes; // gets position node
						
						foreach (XmlNode n in objContent) {
							if (n.Name == "type")
								type = n.InnerText;
							else {
								XmlNodeList pos = n.ChildNodes;
								foreach (XmlNode t in pos) {// x y z nodes
									if (t.Name == "x") {
										x = float.Parse (t.InnerText); // convert the strings to float and apply to the X variable.
									}
									if (t.Name == "y") {
										y = float.Parse (t.InnerText); // convert the strings to float and apply to the Y variable.
									}
									if (t.Name == "z") {
										z = float.Parse (t.InnerText); // convert the strings to float and apply to the Z variable.
									}
								}
							}
						}
						Debug.Log(type + " | " + Resources.Load(type));
						Vector3 v = new Vector3 (x,y,z);
						GameObject o = Resources.Load (type) as GameObject;
						GameObject go = Instantiate(o, v, Quaternion.identity) as GameObject;
						go.name = o.name;
						if (type.Equals("Block2x2")) {
							BlockScript script = (BlockScript) go.GetComponent("BlockScript");
					Debug.Log(v);
							script.CreateBlock(v,new Vector3(0,0,0),go);
						}

					}
					//CubeObject.transform.eulerAngles =new Vector3(X,Y,Z); // Apply the values to the cube object.

				}
		}
	
}
