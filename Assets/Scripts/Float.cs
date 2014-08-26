using UnityEngine;
using System.Collections;

public class Float : MonoBehaviour {

	//Buoyancy factor
	private float buoyancyFactor = 0.5f;

	//The y axis value at which the water is located. In this case its at y = 0
	private float waterLevel = 0.0f;
	
	void FixedUpdate () {

		//If the current object is below the water, apply the forces simulating water
		if (transform.position.y < waterLevel) {

			//Currently this script is simple and is only applying forces in the y direction. 
			rigidbody2D.AddForce (new Vector2 (0, rigidbody2D.mass * Physics.gravity.magnitude * (1 + Mathf.Abs (transform.position.y - waterLevel) * buoyancyFactor)));
			rigidbody2D.AddForce (new Vector2 (0, -.5f * rigidbody2D.mass * buoyancyFactor * rigidbody2D.velocity.y));
		}

		//Else do nothing; gravity will come into play instead
		else{
			rigidbody2D.AddForce ( new Vector2(0,0));
		}
	}
}
