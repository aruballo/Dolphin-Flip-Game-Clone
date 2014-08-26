using UnityEngine;
using System.Collections;

public class Movement2 : MonoBehaviour {

	//This movement script is based on the water actually being simulated using the float script. 

	// initial speed
	private float speed = 3;

	//is the seal coming down from the sky?
	private bool inAir = false;

	void Update()
	{
			
		// Check if user is pressing the up button and that the seal is in water.
		if (Input.GetAxis("Vertical") > 0 && transform.position.y <= 0) {

			//If player is underwater and pressing up, move him forward
			//based on the speed.
			rigidbody2D.AddForce (transform.right * speed);


			//If the player was up in the air and he has entered the water face down
			//it is a succesful dive and the player should get a speed boost. 
			if(inAir == true && (.60 < transform.rotation.z && .78 > transform.rotation.z)){
				speed = speed * 1.2f;
			}

			//If the player hasn't, then the speed should reset
			else{
				speed = 3;
			}


			inAir = false;
		}

		else if (transform.position.y > 0) {
			//do nothihng; player will be able to rotate still but otherwise only gravity should be in effect
			//with the exception of the speed that it came out of the water at.
		    
			//boolean indicating the player is above water
			if(transform.position.y > .4){
				inAir = true;
			}
		}
		
	}
}
