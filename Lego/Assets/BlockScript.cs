using UnityEngine;
using System.Collections;

public class BlockScript: MonoBehaviour {

	public GameObject basicBlock;
	public bool playerCanDestroy = false;
	public int length;
	public int width;
	// Use this for initialization
	void Start () {
		basicBlock = (GameObject)Instantiate (basicBlock, new Vector3 (0,0,0), Quaternion.identity );
	}

	public void CreateBlock(Vector3 position, Vector3 normal, GameObject parent) {
		for (int i = 0; i < length; i++) {
			for (int j = 0; j < width; j++) {
				float scale = (normal.y == 1 || normal.y == -1) ? (float)0.9 : (float)0.75;
				GameObject b = (GameObject)Instantiate (basicBlock, (position + basicBlock.transform.lossyScale.z * new Vector3 (1*i, 0, 1*j) + (normal * scale)), Quaternion.identity);
				b.name = basicBlock.name;
				b.transform.parent = parent.transform;
			}
		}
	}

	public bool canDestroy() {
				return playerCanDestroy;
		}

	public bool canSpawn() {
		return true;
	}
	// Update is called once per frame
	void Update () {
	
	}
}
