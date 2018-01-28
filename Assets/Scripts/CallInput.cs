using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class CallInput : MonoBehaviour {

	Text console;
	string[] callStrings;
	string textModel;
	bool magiTypeOn = true;

	public SwitchingManager switchManager;

	int blinkPeriod = 17;
	int blinkCount = 0;

	int currentLine = 0;
	int currentChar = 0;
	bool isCursorOn = true;

	// Use this for initialization
	void Start () {
		console = this.GetComponent<Text> ();
		textModel = console.text;
	}

	// Update is called once per frame
	void Update () {

		if (currentLine >= callStrings.Length) return;

		if (textModel.Length == 0) {
			string identifier = initNewLine ();
			magiTypeOn = (identifier.Contains ("CENTRAL")||identifier=="");
			textModel += identifier;
		}

		if (magiTypeOn) {
			int numbChars = (Input.inputString.Length)*2;
			int end = currentChar + numbChars;
			if (end > callStrings [currentLine].Length)
				end = callStrings [currentLine].Length;

			for (;currentChar < end; currentChar++) textModel += callStrings [currentLine] [currentChar];
			
			//currentChar;

		} else {
			if (Time.frameCount % 2 == 0) {
				textModel += callStrings [currentLine] [currentChar];
				currentChar++;
			}
		}
			
		if (currentChar >= callStrings [currentLine].Length) {
			if (Input.GetKeyDown ("return") && magiTypeOn || !magiTypeOn) {
				textModel += '\n';
				currentLine++;

				if (currentLine < callStrings.Length) {
					string identifier = initNewLine ();
					magiTypeOn = (identifier.Contains ("CENTRAL") || identifier == "");
					textModel += identifier;
				}
			}
		}

		console.text = textModel;

		if(currentLine >= callStrings.Length){
			if(GameStateManager.Manager().GetGameState()==0){
				GameStateManager.Manager ().PrologueEnded ();
			}
			if(GameStateManager.Manager().GetGameState()==6){
				SceneManager.LoadScene ("Ending_Retaliate");
				return;
			}
			if(GameStateManager.Manager().GetGameState()==7){
				SceneManager.LoadScene ("Ending_StandDown");
				return;
			}
			switchManager.switchToIvan ();
		}

		if (isCursorOn) {
			console.text = console.text + "█";		
		}

		if(blinkCount == blinkPeriod){
			blinkCount = 0;
			isCursorOn = !isCursorOn;
		}

		blinkCount++;
	}

	string initNewLine()
	{
		string newString = callStrings [currentLine];
		if(newString == "")
		{
			textModel += '\n';
			currentLine++;
			return initNewLine ();
		}
		if(callStrings [currentLine][0] == '>')
		{
			currentChar = 0;
			return "";
		}
		
		string startString = "";
		int i = 0;
		for (; i < newString.Length; i++) {
			startString += newString[i];
			if(newString[i] == ':')break;
			
		}
		currentChar = i+1 > newString.Length ? i : i+1;
		if(startString.Equals(newString))
		{
			// Debug.Log ("Here");
			currentChar = 1;
			startString = newString [0].ToString ();
		}
		return startString;	
	}
			

	public void loadCallStrings(string callFile)
	{
		textModel = "";
		StreamReader reader = new StreamReader("Assets/Calls/"+callFile); 
		string content = reader.ReadToEnd ();
		reader.Close();

		callStrings = content.Split ('\n');

		blinkCount = 0;
		currentLine = 0;
		currentChar = 0;
		//maybe fixes the bug?! magiTypeOn = false;
	}

}
