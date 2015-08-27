using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    /*
     * TO DO:
     * Movement
     *      Should use the CharacterController which is already attached to this GameObject
     *      Be able to move left and right
     *      Collide with/be stopped by walls
     *      Not move too quickly or slowly
     *          Remember that movement happens every frame
     * Jumping/Falling
     *      Fall to the ground, and not through it
     *      Able to jump
     *      Can reach platforms to the right, but not the one on the left
     *      Only able to jump while standing on the ground
     * Input
     *      Ideally, use the KeyboardInput script which is already attached to this GameObject
     *      A & D for left and right movement
     *      Space for jumping
     * Moving Platform
     *      When standing on the platform, the character should stay on it/move relative to the moving platform
     *      When not standing on the platform, revert to normal behavior
     * Enemy
     *      If the character touches the enemy, he should "die"
     *      
     * 
     * 
     * 
     * Variables you might want:
     *      References to the CharacterController and Keyboard input classes
     *      Speed values for moving, falling, and jumping
     *      A value to keep track of the player's movement speed and direction
     *      You will probably need to use the Update function as well as create functions for moving platforms and enemies
     */
	private KeyboardInput key;
	private Vector3 moveDirection = Vector3.zero;
	public float speed = 2.0F;
	private bool isOnMovingPlataform = false;
	private MovingPlatform plataform;
	private CharacterController controller;
	void Update()
	{
		key = GetComponent<KeyboardInput> ();
		controller = GetComponent<CharacterController> ();
		
		if (controller.isGrounded && !isOnMovingPlataform) {
			moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= speed;
			if(key.JumpButtonPressedThisFrame)
				moveDirection.y = 10.0F;
		}
		moveDirection.y -= 30.0F * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);
	}
	void OnTriggerEnter(Collider collider){
		Debug.Log ("Collide: " + collider.gameObject.name);
		plataform = GetComponent<MovingPlatform> ();
		if (collider.gameObject.name == "Moving Platform Parent") {
			Debug.Log ("platafor");
			isOnMovingPlataform = true;
			moveDirection = new Vector3 (plataform.transform.position.x, plataform.transform.position.y, plataform.transform.position.z);
			moveDirection.y -= 20.0F * Time.deltaTime;
			controller.Move (moveDirection * Time.deltaTime);
		} else if (collider.gameObject.name == "Enemy Parent")
			Time.timeScale = 0;
	}

}