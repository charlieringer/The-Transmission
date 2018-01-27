using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingManager : MonoBehaviour {

	public GameObject charlieTerminal;
	public GameObject ivanTerminal;

	public void switchToCharlie(string callId){
		charlieTerminal.SetActive (true);
		ivanTerminal.SetActive (false);
		charlieTerminal.GetComponent<CallInput> ().loadCallStrings (callId);
	}

	public void switchToIvan(){
		charlieTerminal.SetActive (false);
		ivanTerminal.SetActive (true);
	}

}
