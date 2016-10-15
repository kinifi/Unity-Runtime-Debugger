using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebuggerTreeView : MonoBehaviour {

	void Traverse(GameObject obj)
	{
		Debug.Log (obj.name);
		foreach (Transform child in obj.transform) 
		{
			Traverse (child.gameObject);
		}

	}

	// Use this for initialization
	void TreeSetup () {

		foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
		{
			if (obj.transform.parent == null)
			{
				Traverse(obj);
			}
		}

	}
 
}
