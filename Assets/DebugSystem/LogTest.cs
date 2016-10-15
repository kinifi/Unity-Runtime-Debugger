using UnityEngine;
using System.Collections;

public class LogTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		StartCoroutine(LogNow());

	}
	
	public IEnumerator LogNow()
	{

		Debug.Log("Log Here");

		yield return new WaitForSeconds(1.0f);

		Debug.Log("Log Second");

		Debug.Log("Log Third");

		yield return new WaitForSeconds(1.0f);

		Debug.LogWarning("First Warning Goes Here");

		yield return new WaitForSeconds(2.0f);

		Debug.LogError("First Error Goes Here");

	}
}
