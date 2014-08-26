using UnityEngine;
using System.Collections;

public class Movement3 : MonoBehaviour {
	//This movement is based more on the way Dolphin olympics works.
	//This script should be used without the float script.
	//Gravity will only apply when the player is above the waterlevel

	//base speed
	public float speed = 7;

	//Water level, in this case y = 0; 
	public float waterLevel = 0.0f;

	//boolean that is changed to true when the player is above water
	private bool inAir = false; 

	//arcTangent value in degrees
	private float arcTangent;

	//current player's rotation degrees (From 0 to 359)
	private float degrees;

	//Animation
	private Animator animator;

	void Awake() {
		animator = GetComponent<Animator>();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.y <= waterLevel) {

			//If the player is underwater, gravity should be turned off
			rigidbody2D.gravityScale = 0;

			//Calculate the arctangent of the velocity the player entered into the water with, 
			//and use it to get the player's current rotation in degrees
			if(inAir == true){
				arcTangent = ((Mathf.Rad2Deg * Mathf.Atan (rigidbody2D.velocity.y/rigidbody2D.velocity.x)));
				if(rigidbody2D.velocity.x > 0 && rigidbody2D.velocity.y > 0){
					degrees = arcTangent;
				}
				else if(rigidbody2D.velocity.x < 0 && rigidbody2D.velocity.y > 0){
					degrees = 90 - arcTangent;
				}
				else if(rigidbody2D.velocity.x < 0 && rigidbody2D.velocity.y < 0){
					degrees = 180 + arcTangent;
				}
				else{
					degrees = 360 + arcTangent;
				}

				//If the player was in the air, and entered the water in a direction closely facing its current velocity, he gets a speed boost
				//transform.localRotation.eulerAngles.z is what you see in the inspector, which is the degrees from 0-359.
				if(degrees - 20 <= transform.localRotation.eulerAngles.z && degrees + 20 >= transform.localRotation.eulerAngles.z){
					speed = speed * 1.2f;
				}

				//If the player was in the air, and he doesnt enter the water facing his velocity vector, 
				//speed gets reset
				else{
					speed = 7;
				}
			}

			//If player is underwater and pressing up, move him forward
			//based on the speed.
			if(Input.GetAxis("Vertical") > 0){
				rigidbody2D.velocity = transform.right * speed;
				animator.SetBool("Moving", true);
			}
			else{
				animator.SetBool("Moving", false);
			}


			//Border logic. 
			if(transform.position.x > 28){
				rigidbody2D.velocity = new Vector2(-2, 0);
				speed = 7;
			}
			else if(transform.position.x < -28){
				rigidbody2D.velocity = new Vector2(2, 0);
				speed = 7;
			}
			else if(transform.position.y < -16){
				rigidbody2D.velocity = new Vector2(0, 2);
				speed = 7;
			}

			inAir = false;
		}

		else{

			//Player is in the air, apply gravity, stop animation
			//and set boolean to true;
			animator.SetBool("Moving", false);
			rigidbody2D.gravityScale = 1;
			inAir = true; 
		}
	}
}
