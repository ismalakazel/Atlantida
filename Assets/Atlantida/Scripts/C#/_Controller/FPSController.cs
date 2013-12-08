using UnityEngine;
using System.Collections;

public class FPSController : MonoBehaviour {
		
	public Joysticks moveTouchPad;
	public Joysticks rotateTouchPad;
	
	public Transform target;
	public Transform Pivot;
	public Camera alternate_camera;
	
	public AudioClip footSteps;
	private float stepLength = 1.3f;
	
	private Transform this_transform;
	private CharacterController character;
	private float forwardSpeed = 7.0f;
	private float backwardSpeed = 7.0f;
	private float sidestepSpeed = 7.0f;
	private Vector3 velocity;
	private Vector2 rotationSpeed = new Vector2( 200, 200 );
	private float elapsedTime = 0f;
	
	private Vector2 camRotation;
	
	// Use this for initialization
	void Start () {
		Pivot.Rotate(Manager.camPivot, 0, 0);
		this_transform = this.transform;
		character = this.transform.GetComponent<CharacterController>();
	}
	
	
	void Awake(){
		StartCoroutine(PlayFootStep());
	}
	
	private IEnumerator PlayFootStep()
	{
		CharacterController controller = GetComponent<CharacterController>();
		while(true)
		{
			if(controller.isGrounded && controller.velocity.magnitude > 0.8f)
			{
				audio.clip = footSteps;
				audio.Play();
				yield return new WaitForSeconds(stepLength);
				
			} else {
				audio.Stop();
				yield return 0;
			}
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		elapsedTime += Time.deltaTime;
		
		Vector3 movement = this_transform.TransformDirection( new Vector3( moveTouchPad.position.x, 0, moveTouchPad.position.y ) );
		movement.y = 0;
		movement.Normalize();

		Vector2 absJoyPos = new Vector2( Mathf.Abs( moveTouchPad.position.x ), Mathf.Abs( moveTouchPad.position.y ) );

		if ( absJoyPos.y > absJoyPos.x )
		{
			if ( moveTouchPad.position.y > 0 ) {
				movement *= forwardSpeed * absJoyPos.y;
			}
			else if (moveTouchPad.position.y < 0) {
				movement *= backwardSpeed * absJoyPos.y;
				
			} else {
			}
		}
		else {
			movement *= sidestepSpeed * absJoyPos.x;
		}
		
		movement += velocity;	
		movement += Physics.gravity;
		movement *= Time.deltaTime;
	
		character.Move(movement);
		camRotation = Vector2.zero;
		camRotation = rotateTouchPad.position;
		camRotation.x *= rotationSpeed.x;
		camRotation.y *= rotationSpeed.y;
		camRotation *= Time.deltaTime;
		
		this_transform.Rotate( 0, camRotation.x, 0, Space.World );	
		Pivot.Rotate( -camRotation.y,0, 0 );	
	}
	
}
