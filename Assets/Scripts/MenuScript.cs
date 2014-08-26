using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour {

	// Use this for initialization
	void OnGUI () {
		const int buttonWidth = 84;
		const int buttonHeight = 60;

		if (
			GUI.Button(
			// Center in X, 2/3 of the height in Y
			new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(2 * Screen.height / 3) - (buttonHeight / 2),
			buttonWidth,
			buttonHeight
			),
			"Start!"
			)
			){
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("MainStage");
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
