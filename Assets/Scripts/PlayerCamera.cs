using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	//Follows the player

	public Transform target;
	public float distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(target.position.x, target.position.y, target.position.z - distance) ;
	}
}
