using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class MapManager : MonoBehaviour {

	private string map11;
	private string map12;
	private string map21;
	private string map22;

	public Text mapContent;

	private bool isOneNotTwo = true;

	private int count = 0;
	private int period = 17;

	// Use this for initialization
	void Start () {
		map11 = LoadFile ("Map1_1.txt");
		map12 = LoadFile ("Map1_2.txt");
		map21 = LoadFile ("Map2_1.txt");
		map22 = LoadFile ("Map2_2.txt");
		count = period;
	}
	
	// Update is called once per frame
	void Update () {

		if(count == period){
			count = 0;

			if(GameStateManager.Manager().GetGameState() < 2){
				if(isOneNotTwo){
					mapContent.text = map11;
				}else{
					mapContent.text = map12;
				}
			}else{
				if(isOneNotTwo){
					mapContent.text = map21;
				}else{
					mapContent.text = map22;
				}
			}
			isOneNotTwo = !isOneNotTwo;
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
