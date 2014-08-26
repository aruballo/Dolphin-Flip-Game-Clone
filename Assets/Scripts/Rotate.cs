using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

	//Default rotation speed
	private float rotationSpeed = 5; 

	//Boolean that is set to true when player immediately leaves water
	private bool leftWater = false;

	//Scoreboard object
	public GUIText scoreboard; 

	//Trick list object
	public GUIText tricklist; 

	//Player total Score
	private float totalScore = 0;

	//Player current Score
	private float currentScore = 0;

	//Reference to movement script
	private Movement3 movement;

	//difference between initial rotation and current
	private float delta = 0;

	//current Rotation
	private float currentRotation = 0;

	//previous Rotation
	private float previousRotation = 0;

	//total amount of rotation Rotation
	private float totalRotation = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//Movement is a reference to the movement3 script, which controls 
		//player motions and gravity, as well as the water level.
		movement = this.gameObject.GetComponent<Movement3> ();

		//if the player is currently underwater, set left water to false
		//, delta to 0, and total rotation to 0. This is needed so that 
		//whenever the player enters the water the values are reset from the
		//previous flipping
		if(transform.position.y < movement.waterLevel){
			leftWater = false; 
			delta = 0;
			totalRotation = 0;
		}

		//Check if player is pressing left or right, figure it out and rotate accordingly. 
		if (Input.GetAxis("Horizontal") > 0) {
			RotateRight();
		}
		if (Input.GetAxis("Horizontal") < 0) {
			RotateLeft();
		}
	}

	void RotateRight(){

		//If rotating above water
		if (transform.position.y > movement.waterLevel) {
			//Get current rotation
			currentRotation = transform.localRotation.eulerAngles.z;	

			//If player just jumped out of the water, set the initial 
			//values for initial rotation and previousRotation
			//then set the leftwater boolean to false so that 
			//this code is not run on next rotate while still in air.
			if(!leftWater){
				previousRotation = currentRotation;
				leftWater = true;
			}

			//I decided that rotating right should result in a positive delta,
			//and rotating left should result in a negative delta. 
			delta = previousRotation - currentRotation;

			//If delta is positive then add it to the total rotation.
			if(delta > 0){
				totalRotation = totalRotation + delta;
			}

			//If delta is negative, check to see if the player has passed the 359 degrees
			//mark and is now in between 0 and 20. This would indicate the degrees are simply
			//starting at 0 again.
			else if(delta < 0){
				if(currentRotation >= 350 && previousRotation <= 20){
					totalRotation = totalRotation + (360 - currentRotation) + previousRotation;
					
				}

				//if not, something has gone wrong, since this case should never be reached
				else{
					//Debug.Log ("Error: Negative Delta in RotateRight");
					//totalRotation = totalRotation + Mathf.Abs(delta);
				}
			}

			//If the player has completed a full 360, the score should go up.
			//Totalrotation should go back to 0
			if(totalRotation/360 >= 1){
				currentScore++;
				tricklist.text = "Rotation x " + currentScore;
				totalRotation = 0;
			}

			//Set the previousRotation to what the currentRotation
			//for the next run of this function
			previousRotation = currentRotation;

			//If the player is flying straight up, apply some "drag"
			//to the rotations
			if(0 <= Mathf.Abs(rigidbody2D.velocity.x) && Mathf.Abs(rigidbody2D.velocity.x) <= 2 && 0 <= Mathf.Abs(rigidbody2D.velocity.y)&& Mathf.Abs(rigidbody2D.velocity.x) <= 2){
				rotationSpeed = rotationSpeed - .02f;
				if(rotationSpeed < 3){
					rotationSpeed = 3;
				}
			}
			else{
				rotationSpeed = rotationSpeed + .02f;
				if(rotationSpeed > 5){
					rotationSpeed = 5;
				}
			}


		}
		else{
			if(movement.speed == 7){
				currentScore = 0;
			}
			tricklist.text = "";
			totalScore = totalScore + currentScore;
			scoreboard.text = "Score: " + totalScore;
			rotationSpeed = 5;
			currentScore = 0;

		}

		transform.Rotate (Vector3.back * rotationSpeed);
	}

	void RotateLeft(){
		//Mostly same logic as the RotateRight() function
		//with the exception of using negative values for delta
		//and absolute values when appropriate
		if (transform.position.y > movement.waterLevel) {
			currentRotation = transform.localRotation.eulerAngles.z;	
			if(!leftWater){
				previousRotation = currentRotation;
				leftWater = true;
			}
				
			delta = previousRotation - currentRotation;
				
			if(delta < 0){
				totalRotation = totalRotation + delta;
			}
			else if(delta > 0){
				if(previousRotation >= 350 && currentRotation <= 20){
					totalRotation = totalRotation + (360 - previousRotation) + currentRotation;
				}
				else{
					//Debug.Log ("Error: Positive Delta on RotateLeft");
					//totalRotation = totalRotation - Mathf.Abs(delta);
				}
			}

			if(Mathf.Abs(totalRotation/360) >= 1){
				currentScore++;
				tricklist.text = "Rotation x " + currentScore;
				totalRotation = 0;
			}

			previousRotation = currentRotation;

			if(0 <= rigidbody2D.velocity.x && Mathf.Abs(rigidbody2D.velocity.x) <= 2 && 0 <= rigidbody2D.velocity.y && Mathf.Abs(rigidbody2D.velocity.x) <= 2){
				rotationSpeed = rotationSpeed - .02f;
				if(rotationSpeed < 3){
					rotationSpeed = 3;
				}
			}
			else{
				rotationSpeed = rotationSpeed + .02f;
				if(rotationSpeed > 5){
					rotationSpeed = 5;
				}
			}
		}
		else{
			if(movement.speed == 7){
				currentScore = 0;
			}
			tricklist.text = "";
			totalScore = totalScore + currentScore;
			scoreboard.text = "Score: " + totalScore;
			rotationSpeed = 5;
			currentScore = 0;
		}

		transform.Rotate (Vector3.forward * rotationSpeed);
	}
}
