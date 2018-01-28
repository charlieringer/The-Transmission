using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingManager : MonoBehaviour {

	public GameObject charlieTerminal;
	public GameObject ivanTerminal;
	public CallInput charlieCall;
	public CommandLine ivanCommandLine;

	public void switchToCharlie(string callId){
		charlieTerminal.SetActive (true);
		ivanTerminal.SetActive (false);
		charlieCall.loadCallStrings (callId);
	}

	public void switchToIvan(){
		charlieTerminal.SetActive (false);
		ivanTerminal.SetActive (true);
		string nextPrint = GameStateManager.Manager ().GetNextPrint ();
		Debug.Log (GameStateManager.Manager ().GetGameState() +" - "+ nextPrint);
		ivanCommandLine.InstantiatePrint (nextPrint);
	}

}
