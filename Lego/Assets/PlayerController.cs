﻿using UnityEngine;
using System.Collections;


//TODO: Add script attached to basic brick. defines surfaces that bricks can be attached to, canPutBrick(vec3 	normal), 


public class PlayerController : MonoBehaviour {

	public GameObject block;
	RaycastHit hit;

	// Use this for initialization
	void Start () {
	}
	 		
	// Update is called once per frame
	void Update () {
		// debug aim
		Camera cam = Camera.main;
		Vector3 p1 = cam.transform.position + Vector3.Normalize(Vector3.Cross(cam.transform.forward, cam.transform.up));
		Vector3 p2 = cam.transform.position - Vector3.Normalize (Vector3.Cross(cam.transform.forward, cam.transform.up));
		Debug.DrawRay (p1, Quaternion.AngleAxis(23/2,cam.transform.up)*cam.transform.forward * 5, Color.red);
		Debug.DrawRay (p2, Quaternion.AngleAxis(-23/2,cam.transform.up)*cam.transform.forward * 5, Color.red);
		
		if (Input.GetMouseButtonDown (0)) {
			print ("mouseDown(1)");
			PlaceBlock ();
		}
		if (Input.GetMouseButtonDown (1)) {
			print ("mouseDown(0)");
			RemoveBlock ();
		}

	}

	void PlaceBlock() {
		if (HitBlock()) {

			//float scale = (hit.normal.y == 1 || hit.normal.y == -1) ? (float)0.9 : (float)0.75;
			//Block b = Instantiate(block);
			string bn = block.name;
			GameObject b = (GameObject)Instantiate(block);
			b.name = bn;
			BlockScript script = (BlockScript) b.GetComponent("BlockScript");
			script.CreateBlock(hit.transform.position,hit.normal,b);

			/*
			//                 position             + 0.9 in direction of hit      no rotation  
			Instantiate(block, hit.transform.position + hit.normal  * scale, Quaternion.identity);
			Instantiate(block, hit.transform.position + block.transform.lossyScale.z * new Vector3(0,0,1) + hit.normal * scale, Quaternion.identity);
			Instantiate(block, hit.transform.position + block.transform.lossyScale.z * new Vector3(1,0,0) + hit.normal * scale, Quaternion.identity);
			Instantiate(block, hit.transform.position + block.transform.lossyScale.z * new Vector3(1,0,1) + hit.normal * scale, Quaternion.identity);
			*/
		}
	}

	void RemoveBlock() {
		if(HitBlock()) {
			if (hit.transform.parent != null) {
				BlockScript script = (BlockScript) hit.transform.parent.GetComponent("BlockScript");
				if (script.canDestroy())
					Destroy(hit.transform.parent.gameObject);
			}
		}
	}
	
	bool HitBlock() {
		if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,5,1)){
			Debug.Log (hit.transform.name);
			if (hit.transform.name.Equals("1x1block") && hit.transform != null) {
				print (hit.normal);
				//if (hit.normal == new Vector3(0,1,0) || hit.normal == new Vector3(0,-1,0)) {
					return true;
				//}
			}
		}
		return false;
	}	
}