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
	
	private string x = ""; // string to work with the xml file.
	private string y = ""; // string to work with the xml file.
	private string z = ""; // string to work with the xml file.
	
	private float X = 0; // we will apply the values ​​of the strings here.
	private float Y = 0; // we will apply the values ​​of the strings here.
	private float Z = 0; // we will apply the values ​​of the strings here.

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
			xmlDoc.CreateElement ("Level");
		} else {
			xmlDoc.Load (filepath);
		}
		XmlElement elmRoot = xmlDoc.DocumentElement;

		elmRoot.RemoveAll (); // remove all inside the transforms node.

		XmlElement elmNew = xmlDoc.CreateElement ("rotation"); // create the rotation node.

		XmlElement rotation_X = xmlDoc.CreateElement ("x"); // create the x node.
		rotation_X.InnerText = x; // apply to the node text the values of the variable.

		XmlElement rotation_Y = xmlDoc.CreateElement ("y"); // create the y node.
		rotation_Y.InnerText = y; // apply to the node text the values of the variable.

		XmlElement rotation_Z = xmlDoc.CreateElement ("z"); // create the z node.
		rotation_Z.InnerText = z; // apply to the node text the values of the variable.

		elmNew.AppendChild (rotation_X); // make the rotation node the parent.
		elmNew.AppendChild (rotation_Y); // make the rotation node the parent.
		elmNew.AppendChild (rotation_Z); // make the rotation node the parent.
		elmRoot.AppendChild (elmNew); // make the transform node the parent.

		xmlDoc.Save (filepath); // save file.
	}
	
	public void LoadFromXml()
	{
				string filepath = Application.dataPath + @"/Data/gamexmldata.xml";
				XmlDocument xmlDoc = new XmlDocument ();
		
				if (File.Exists (filepath)) {
						xmlDoc.Load (filepath);

						XmlNodeList transformList = xmlDoc.GetElementsByTagName ("rotation");

						foreach (XmlNode transformInfo in transformList) {
								XmlNodeList transformcontent = transformInfo.ChildNodes;

								foreach (XmlNode transformItens in transformcontent) {
										if (transformItens.Name == "x") {
												X = float.Parse (transformItens.InnerText); // convert the strings to float and apply to the X variable.
										}
										if (transformItens.Name == "y") {
												Y = float.Parse (transformItens.InnerText); // convert the strings to float and apply to the Y variable.
										}
										if (transformItens.Name == "z") {
												Z = float.Parse (transformItens.InnerText); // convert the strings to float and apply to the Z variable.
										}
								}
						}
						//CubeObject.transform.eulerAngles =new Vector3(X,Y,Z); // Apply the values to the cube object.

				}
		}
	
}
