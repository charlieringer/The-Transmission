using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallManager : MonoBehaviour {

	int northStatus = 0;
	int westStatus = 0;
	int eastStatus = 0; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void handleMissileAlert()
	{
		northStatus = westStatus = eastStatus = 1;
	}

	void handlePasswordDiscovered()
	{
		northStatus = westStatus = eastStatus = 2;
	}

	void handlePasswordReceived(string location)
	{
		if (location.Contains ("north")) {
			northStatus = 3;
		} else if (location.Contains ("east")) {
			eastStatus = 3;
		} else if (location.Contains ("west")) {
			westStatus = 3;
		}
	}
}