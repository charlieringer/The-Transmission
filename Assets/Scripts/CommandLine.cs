using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface FeedMeTextPlease
{
	void feedText(string str);
}


public class CommandLine : MonoBehaviour, FeedMeTextPlease {

	private TextFeeder feeder;

	private string textToWrite;
	private string consoleText;

	int blinkPeriod = 17;
	int blinkCount = 0;
	bool isCursorOn = true;

	int textRate = 3;
	int textRateCount = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(textRateCount == textRate){
			consoleText = consoleText + textToWrite [0];
			textToWrite = textToWrite.Substring (1);
			textRate = 0;	
		}

		if (isCursorOn) {
			consoleText = consoleText + "█";	
		}

		if(blinkCount == blinkPeriod){
			blinkCount = 0;
			isCursorOn = !isCursorOn;
		}

		blinkCount++;
		textRate++;
	}

	public void feedText(string str){
		textToWrite = textToWrite + "\n" + str;
	}
}
