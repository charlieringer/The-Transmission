using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd : TextFeeder {


	private string currentCommand = "";
	private static string invalidCommand = "\nInvalid command.\n";

	public Cmd(FeedMeTextPlease feedMe):base(feedMe){
	
	}

	public override void KeyboardInput(string str){

		for(int i=0; i<str.Length; i++){
			string tmp = ""+str[i];


			if (tmp.Equals ("\n")) {
				checkCommand (currentCommand);
				break;
			} else if (tmp.Equals ("\b")) {
				if (currentCommand.Length > 1) {
					currentCommand = currentCommand.Remove (currentCommand.Length - 1);
					feedMe.FeedText (tmp);
				} else {
					currentCommand = "";
					feedMe.FeedText (tmp);
				}
			} else {
				feedMe.FeedText(tmp);
				currentCommand = currentCommand + tmp;
			}

		}

	
	}

	public override void ProvideContent(){
	}

	private void checkCommand(string command){

		Debug.Log("Check command: "+command);

		string[] tokens = command.Split(' ');
		string cmd = tokens [0];

		Debug.Log("cmd: "+cmd+" l: "+cmd.Length);
		Debug.Log (cmd.Equals("HELP"));

		if (cmd.Equals ("print")) {
			feedMe.Exit ();
			Debug.Log ("PRINT recognised");
			this.feedMe.InstantiatePrint (tokens [1]);
		} else if (cmd.Equals ("call")) {
			feedMe.Exit ();
			Debug.Log ("CALL recognised");
			this.feedMe.InstantiateCall (tokens [1]);
		} else if (cmd.Equals ("HELP")) {
			Debug.Log ("HELP recognised");
			feedMe.Exit ();
			this.feedMe.InstantiatePrint (cmd);
		} else {
		
			feedMe.FeedText (invalidCommand);
		}



	}

}
