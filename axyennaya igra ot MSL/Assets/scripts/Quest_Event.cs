using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest_Event : MonoBehaviour {
	public bool Quest1;
	public GameObject Text1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Quest1 == true) {
			Text1.SetActive (true);
		} else {
			Text1.SetActive (false);
		}
	}
}