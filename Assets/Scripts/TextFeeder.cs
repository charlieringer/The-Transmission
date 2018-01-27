using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TextFeeder {

	protected FeedMeTextPlease feedMe;

	public TextFeeder(FeedMeTextPlease feedMe){
		this.feedMe = feedMe;
	}

	abstract public void ProvideContent();

	abstract public void KeyboardInput(string str);

}
