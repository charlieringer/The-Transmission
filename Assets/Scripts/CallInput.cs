using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class CallInput : MonoBehaviour {



	public string filePath;
	Text console;
	string[] callStrings;
	string textModel;
	bool magiTypeOn = true;



	int blinkPeriod = 17;
	int blinkCount = 0;

	int currentLine = 0;
	int currentChar = 0;
	bool isCursorOn = true;

	// Use this for initialization
	void Start () {
		console = this.GetComponent<Text> ();
		textModel = console.text;
		loadCallStrings (filePath);
	}

	// Update is called once per frame
	void Update () {

		if (currentLine >= callStrings.Length) return;

		if (textModel.Length == 0) {
			string identifier = initNewLine ();
			magiTypeOn = identifier.Contains ("CENTRAL");
			textModel += identifier;
		}

		if (magiTypeOn) {
			int numbChars = (Input.inputString.Length)*2;
			int end = numbChars + currentChar;
			if (end > callStrings [currentLine].Length)
				end = callStrings [currentLine].Length;

			for (int i = currentChar; i < end; i++) {
				textModel += callStrings [currentLine] [i];
			}
			currentChar = end;

		} else {
			if (Time.frameCount % 3 == 0) {
				textModel += callStrings [currentLine] [currentChar];
				currentChar++;
			}
		}
			
		if (currentChar == callStrings [currentLine].Length) {
			textModel += '\n';
			currentLine++;

			if (currentLine < callStrings.Length) 
			{
				string identifier = initNewLine ();
				magiTypeOn = identifier.Contains ("CENTRAL");
				textModel += identifier;
			}
		}

		console.text = textModel;

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
		string startString = "";
		int i = 0;
		for (; i < newString.Length; i++) {
			startString += newString[i];
			if(newString[i] == ':')break;
			
		}
		currentChar = i+1 > newString.Length ? i : i+1;
		return startString;	
	}
			

	private void loadCallStrings(string callFile)
	{
		string path = Directory.GetCurrentDirectory();
		if (File.Exists(callFile))
		{
			callStrings = File.ReadAllLines(callFile);
		}
	}
}
