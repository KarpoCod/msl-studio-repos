using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Task : MonoBehaviour {
	public bool EndDialog;
	public GameObject Dialog1;
	public Quest_Event QE;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (EndDialog == true) {
			Time.timeScale = 1;
			QE.Quest1 = true;
			Dialog1.SetActive (false);
		}
	}
	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Player") {
			Time.timeScale = 0;
			Dialog1.SetActive (true);
		}
	}
}