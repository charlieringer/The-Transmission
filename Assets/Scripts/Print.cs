using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Print :  TextFeeder{

	private string content;

	public Print(FeedMeTextPlease feedMe, string printFilePath):base(feedMe){
		loadContent (printFilePath);
	}

	public override void ProvideContent (){
		feedMe.FeedText(content);
		content = "";
		feedMe.Exit ();
	}

	private void loadContent(string filePath){

		StreamReader reader = new StreamReader("Assets/Print/"+filePath+".txt"); 
		content = reader.ReadToEnd ();
		reader.Close();

	}


	public override void KeyboardInput(string str){
		return;
	}

}
