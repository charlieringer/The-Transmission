using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SineManager : MonoBehaviour {

	private string wave0;
	private string wave1;
	private string wave2;
	private string wave3;
	private string wave4;
	private string wave5;

	private string sine0;
	private string sine1;
	private string sine2;

	public Text content;

	private int waveNum = 1;
	private int waveCount = 0;
	private int wavePeriod = 10;
	private string waveContent;

	private int sineNum = 1;
	private int sineCount = 0;
	private int sinePeriod = 10;
	private string sineContent;

	// Use this for initialization
	void Start () {
		wave0 = LoadFile("S1.txt");
		wave1 = LoadFile("S2.txt");
		wave2 = LoadFile("S3.txt");
		wave3 = LoadFile("S4.txt");
		wave4 = LoadFile("S5.txt");
		wave5 = LoadFile("S6.txt");
		sine0 = LoadFile ("W1.txt");
		sine1 = LoadFile ("W2.txt");
		sine2 = LoadFile ("W3.txt");
		waveCount = wavePeriod;
		sineCount = sinePeriod;
	}
	
	// Update is called once per frame
	void Update () {
		if(waveCount == wavePeriod){

			if(waveNum == 0){
				waveContent = wave0;
			}else if(waveNum == 1){
				waveContent = wave1;
			}else if(waveNum == 2){
				waveContent = wave2;
			}else if(waveNum == 3){
				waveContent = wave3;
			}else if(waveNum == 4){
				waveContent = wave4;
			}else if(waveNum == 5){
				waveContent = wave5;
			}
			waveNum = (waveNum + 1) % 6;
			waveCount = 0;
		}

		if(sineCount == sinePeriod){

			if(sineNum == 0){
				sineContent = sine2;
			}else if(sineNum == 1){
				sineContent = sine1;
			}else if(sineNum == 2){
				sineContent = sine0;
			}
			sineNum = (sineNum + 1) % 3;
			sineCount = 0;
		}

		waveCount++;
		sineCount++;

		content.text = waveContent+"\n"+sineContent;
	}

	private string LoadFile(string path){
		StreamReader reader = new StreamReader("Assets/Map/"+path); 
		string ret = reader.ReadToEnd ();
		reader.Close();
		return ret;
	}
}
