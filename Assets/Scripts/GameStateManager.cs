using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager {

	private int gameState = 3; // 0 3 5
	private string genericError = "generic_error.txt";

	private bool original = false;

	// GAME STATE == 1
	bool calledS0_GS1 = false;
	bool calledN0_GS1 = false;
	bool calledE4_GS1 = false;
	bool calledW3_GS1 = false;
	bool calledCOMMAND_GS1 = false;
	// GAME STATE == 2
	bool calledN0_GS2 = false;
	bool calledE4_GS2 = false;
	bool calledW3_GS2 = false;
	// GAME STATE == 3

	private static GameStateManager manager;

	private GameStateManager(){
	}

	public static GameStateManager Manager(){
		if(manager == null){
			manager = new GameStateManager();
		}
		return manager;
	}

	public int GetGameState(){
		return gameState;
	}

	public bool CanRetaliate(){
		return gameState >= 1;
	}

	public string Retaliate(string code){
		if(code.Equals("true")){ //TODO: change true to the actual code
			gameState = 6;
			return "suRetaliate.txt";
		}
		return "retaliate_fail.txt";
	}

	public string GetPrologueCallFile(){
		return "conversations_1"+(original?"_original":"")+".txt";
	}

	public void PrologueEnded(){
		gameState = 1;
	}

	public bool CanCallCommand(){
		if(gameState == 1){
			if(calledE4_GS1 && calledN0_GS1 && calledS0_GS1 && calledW3_GS1){
				return true;
			}
		}
		return false;
	
	}

	public bool CanOverride(){
		return gameState == 2;
	}

	public bool CanSutransmit(){
		return gameState == 4;
	}

	public string GetSutransmitCall(){
		gameState = 5;
		return "sutransmission.txt";
	}

	public string Policy(){
		return "retaliation_protocol.txt";
	}

	public string Override(string code){

		if (code.Equals ("4632833497113204")) { //TODO: change true to the actual code
			gameState = 3;
			return "S0_override_success.txt";
		}

		return "S0_override_fail.txt";
	}

	public string GetOverrideHelp(){
		return "override_help.txt";
	}

	public bool IsSuState(){
		return gameState == 5;
	}

	public string GetCallFile(string callId){
		if(gameState == 1){
			if (callId.Equals ("N0")) {
				calledN0_GS1 = true;
				return "N0_2"+(original?"_original":"")+".txt";
			} else if (callId.Equals ("E4")) {
				calledE4_GS1 = true;
				return "E4_2"+(original?"_original":"")+".txt";
			} else if (callId.Equals ("S0")) {
				calledS0_GS1 = true;
				return "S0_2"+(original?"_original":"")+".txt";
			} else if (callId.Equals ("W3")) {
				calledW3_GS1 = true;
				return "W3_2"+(original?"_original":"")+".txt";
			} else if (callId.Equals ("COMMAND")) {
				if(CanCallCommand ()){
					calledCOMMAND_GS1 = true;
					gameState = 2;
					Debug.Log ("Updating gameState: "+gameState);
					return "command_2"+(original?"_original":"")+".txt";
				}else{
					return "contact_others.txt";
				}
			}else{
				return genericError;
			}

		}if(gameState==2){
			if (callId.Equals ("N0")) {
				if (calledN0_GS2) {
					return "N0_3.1"+(original?"_original":"")+".txt";
				} else {
					calledN0_GS2 = true;
					return "N0_3"+(original?"_original":"")+".txt";
				}
			} else if (callId.Equals ("E4")) {
				if (calledE4_GS2) {
					return "E4_3.1"+(original?"_original":"")+".txt";
				} else {
					calledE4_GS2 = true;
					return "E4_3"+(original?"_original":"")+".txt";
				}
			} else if (callId.Equals ("S0")) {
				return "S0_2"+(original?"_original":"")+".txt";
			} else if (callId.Equals ("W3")) {
				if (calledW3_GS2) {
					return "W3_3.1"+(original?"_original":"")+".txt";
				} else {
					calledW3_GS2 = true;
					return "W3_3"+(original?"_original":"")+".txt";
				}
			} else {
				return genericError;
			}
		}if(gameState == 5){
			if(callId.Equals("suRetaliate")){
				gameState = 6;
				return "suRetaliate.txt";
			}else if(callId.Equals("suStandDown")){
				gameState = 7;
				return "suWait.txt";
			}
		}

		return genericError;
	}

	public string GetNextPrint(){
		if(gameState == 1){
			return "console_main_2.txt";
		}else if (gameState == 2){
			return "console_main_3.txt";
		}else if(gameState == 3){
			gameState = 4;
			return "console_main_4.txt";
		} else if(gameState == 4){
			return "console_main_4.txt";
		} else if(gameState == 5){
			return "console_main_5.txt";
		} else if(gameState == 6){
			return "credits.txt";
		}
		return genericError;
	
	}
}
