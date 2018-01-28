using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class WaveManager : MonoBehaviour {

	private string wave0;
	private string wave1;
	private string wave2;
	private string wave3;

	private int waveNum = 1;
	private int count = 0;
	private int period = 10;

	public Text waveContent;

	// Use this for initialization
	void Start () {
		wave0 = LoadFile("Wave1.txt");
		wave1 = LoadFile("Wave2.txt");
		wave2 = LoadFile("Wave3.txt");
		wave3 = LoadFile("Wave4.txt");
		count = period;
	}
	
	// Update is called once per frame
	void Update () {

		if(count == period){

			if(waveNum == 0){
			}else if(waveNum == 1){
			}else if(waveNum == 2){
			}else if(waveNum == 3){
			}

			waveNum = (waveNum + 1) % 4;
			count = 0;
		}

		count++;
	}


	private string LoadFile(string path){
		StreamReader reader = new StreamReader("Assets/Map/"+path); 
		string ret = reader.ReadToEnd ();
		reader.Close();
		return ret;
	}
}
