using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Threading;


public class PlayerScript : MonoBehaviour {

	private Stopwatch stopwatch;
	// Use this for initialization
	void Start () {
		stopwatch = new Stopwatch ();
		stopwatch.Start ();
	}

	void OnDestroy(){
		
	}
	
	// Update is called once per frame
	void Update () {
		UnityEngine.Debug.Log (stopwatch.ElapsedMilliseconds);
		if (stopwatch.ElapsedMilliseconds > 1200000) {
			stopwatch.Stop ();
		}
	}
}
