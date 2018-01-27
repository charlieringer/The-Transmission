using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public interface FeedMeTextPlease
{
	void FeedText(string str);
	void InstantiateCall (string callId);
	void InstantiatePrint (string callId);
	void Exit();
}


public class CommandLine : MonoBehaviour, FeedMeTextPlease {

	private TextFeeder feeder;

	public Text commandLineText;

	private string textToWrite;
	private string consoleText;

	int blinkPeriod = 17;
	int blinkCount = 0;
	bool isCursorOn = true;

	int textRate = 1;
	int textRateCount = 0;

	// Use this for initialization
	void Start () {
		feeder = new Print (this, "Prologue");
		feeder.ProvideContent ();
	}
	
	// Update is called once per frame
	void Update () {

		// Debug.Log (this.feeder+" text left: "+textToWrite.Length);

		if(this.feeder  == null && textToWrite.Length == 0){
			feeder = new Cmd (this);
		}

		if(this.feeder != null){
			feeder.ProvideContent ();
		}

		if(feeder != null && Input.inputString.Length>0){
			feeder.KeyboardInput (Input.inputString);
		}
	
		if(textRateCount >= textRate){

			if(textToWrite.Length > 0){
				consoleText = consoleText + textToWrite [0];
				textToWrite = textToWrite.Substring (1);
				textRateCount = 0;
			}
		}

		commandLineText.text = consoleText;

		if (isCursorOn) {
			commandLineText.text = commandLineText.text + "█";	
		}

		if(blinkCount == blinkPeriod){
			blinkCount = 0;
			isCursorOn = !isCursorOn;
		}
			
		blinkCount++;
		textRateCount++;
	}

	public void FeedText(string str){
		if (str.Equals ("\b")) {

			if (textToWrite.Length == 0) {
				string lastConsoleChar = "" + consoleText [consoleText.Length - 1];
				if (!lastConsoleChar.Equals ("\n")) {
					consoleText = consoleText.Remove (consoleText.Length - 1);
				}
				
			}
			if (textToWrite.Length > 1) {
				textToWrite = textToWrite.Remove (textToWrite.Length - 2);
			} else {
				textToWrite = "";
			}

		} else {
			textToWrite = textToWrite + str;
		}
	}

	public void InstantiateCall (string callId){
	
		//this.feeder = new CallInput (this, callId);
		
	}

	public void InstantiatePrint (string callId){
		this.feeder = new Print (this, callId);
	}

	public void InstantiateCmd(){
	
		this.feeder = new Cmd (this);
	
	}

	public void Exit(){
		this.feeder = null;
		FeedText ("\n");
	}

}
