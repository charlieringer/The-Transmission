using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cmd : TextFeeder {


	private string currentCommand = "";
	private static string invalidCommand = "\nInvalid command.\n";

	private bool canRetaliate = false;
	private bool canUseFinalCode = false;
	private bool canOverride = false;
	private bool canSutransmit = false;

	public Cmd(FeedMeTextPlease feedMe, bool canRetaliate, bool canUseFinalCode, bool canOverride, bool canSutransmit):base(feedMe){
		this.canRetaliate = canRetaliate;
		this.canOverride = canOverride;
		this.canUseFinalCode = canUseFinalCode;
		this.canSutransmit = canSutransmit;
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

		// Debug.Log ("Check command: " + command);

		string[] tokens = command.Split (' ');
		string cmd = tokens [0];

//		Debug.Log ("cmd: " + cmd+" GS: "+GameStateManager.Manager().GetGameState()+
//			"\n canRet: "+canRetaliate+
//			"\n canOR: "+canOverride+
//			"\n canFinal: "+canUseFinalCode+
//			"\n canSu: "+canSutransmit);

		Debug.Log (cmd+" GS: "+GameStateManager.Manager ().GetGameState());

		if (!GameStateManager.Manager ().IsSuState ()) {
			if (cmd.Equals ("SatNet.Alsys")) {
				feedMe.Exit ();
				Debug.Log ("SatNet.Alsys recognised");
				this.feedMe.InstantiatePrint (cmd);
			} else if (cmd.Equals ("SatNet.transmit")) {
				feedMe.Exit ();
				Debug.Log ("SatNet.transmit recognised");
				this.feedMe.InstantiateCall (tokens [1]);
			} else if (cmd.Equals ("HELP")) {
				Debug.Log ("HELP recognised");
				feedMe.Exit ();
				this.feedMe.InstantiatePrint (cmd);
			} else if (canRetaliate && cmd.Equals ("SatNet.RETALIATE")) {
				Debug.Log ("SatNet.RETALIATE recognised");
				feedMe.Exit ();
				this.feedMe.InstantiateRetaliation (tokens [1]);
			} else if (canOverride && cmd.Equals ("SatNet.override.help")) {
				Debug.Log ("SatNet.override.help recognised");
				feedMe.Exit ();
				this.feedMe.OverrideHelp ();
			} else if (canOverride && cmd.Equals ("SatNet.override")) {
				Debug.Log ("SatNet.override recognised");
				feedMe.Exit ();
				this.feedMe.Override (tokens [1]);
			} else if (cmd.Equals ("SatNet.policy")) {
				Debug.Log ("SatNet.policy recognised");
				feedMe.Exit ();
				this.feedMe.Policy ();
			} else if (canSutransmit && cmd.Equals ("SatNet.sutransmit")) {
				Debug.Log ("SatNet.sutransmit recognised");
				feedMe.Exit ();
				this.feedMe.Sutransmit ();
			} else {
				Debug.Log ("nothing recognised");
				currentCommand = "";
				feedMe.FeedText (invalidCommand);
			}
		} else {
			if (cmd.Equals ("SatNet.suRetaliate")) {
				feedMe.Exit ();
				Debug.Log ("SatNet.suRetaliate recognised");
				this.feedMe.InstantiateCall ("suRetaliate");
			} else if (cmd.Equals ("SatNet.policy")) {
				Debug.Log ("SatNet.policy recognised");
				feedMe.Exit ();
				this.feedMe.Policy ();
			} else if (cmd.Equals ("SatNet.suStandDown")) {
				feedMe.Exit ();
				Debug.Log ("SatNet.suStandDown recognised");
				this.feedMe.InstantiateCall ("suStandDown");
			} else {
				Debug.Log ("nothing recognised");
				currentCommand = "";
				feedMe.FeedText ("You have only to options! Either suRetaliate or suStandDown");
			}	
		}
	}
}
