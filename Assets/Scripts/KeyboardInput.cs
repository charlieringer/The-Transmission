using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardInput : MonoBehaviour {

	Text console;
	string textModel;

	int blinkPeriod = 17;
	int blinkCount = 0;
	bool isCursorOn = true;

	// Use this for initialization
	void Start () {
		console = this.GetComponent<Text> ();
		textModel = console.text;
	}
	
	// Update is called once per frame
	void Update () {
		
		textModel = textModel + Input.inputString;


		if(Input.GetKeyUp(KeyCode.Backspace)){
			textModel = textModel.Remove (console.text.Length - 2);
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
}
