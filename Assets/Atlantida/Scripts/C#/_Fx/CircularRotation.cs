using UnityEngine;
using System.Collections;

public class CircularRotation : MonoBehaviour {
	
	LevelManager lm;
    private Vector2 previousMousePosition = Vector2.zero;
    private Vector2 currentMousePosition = Vector2.zero;
    private bool previousFrameMouseDown = false;
	private LayerMask interactiveLayer = 1 << 10;
    private float originalRot;

    void Start()
    {
		lm = (LevelManager)FindObjectOfType(typeof(LevelManager));
		originalRot = transform.rotation.x;
    }
    
    private void Update() 
    {
		if(lm.altCamera.enabled){
		foreach (Touch touch in Input.touches) {
			if (touch.tapCount <= 1) {
		        RaycastHit hit;
		        Ray ray = lm.altCamera.ScreenPointToRay(Input.mousePosition);
		        if (Physics.Raycast (ray, out hit, Mathf.Infinity, interactiveLayer))
			        if (hit.collider.name == "Calendar") {
			
						Vector2 mousePos = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);
			        
			            if (Input.GetMouseButtonDown (0) && !previousFrameMouseDown) {
			                previousMousePosition = mousePos;
			                currentMousePosition = mousePos;
			                previousFrameMouseDown = true;
			            } else if (Input.GetMouseButton (0) && previousFrameMouseDown) {
			                previousMousePosition = currentMousePosition;
			                currentMousePosition = mousePos;
			            } else if (!Input.GetMouseButton (0)) {
			                previousFrameMouseDown = false;    
			            }
			        
			            Vector3 screenPosition = lm.altCamera.WorldToScreenPoint (transform.position);
			            Vector2 screenPositionXY = new Vector2 (screenPosition.x, screenPosition.y);
			            Vector2 previousPositionVector = previousMousePosition - screenPositionXY;
			            Vector2 currentPositionVector = currentMousePosition - screenPositionXY;
			        
			            if (previousPositionVector != -currentPositionVector && previousFrameMouseDown) {
			                float rotationAmount = ReturnSignedAngleBetweenVectors (previousPositionVector, currentPositionVector);
			                transform.Rotate (Vector3.up, rotationAmount * Time.deltaTime * 25f);
			            }
			        }
				}
			}
		}
    }
    
    
    private float ReturnSignedAngleBetweenVectors(Vector2 vectorA, Vector2 vectorB)
    {

        Vector3 vector3A = new Vector3(vectorA.x, vectorA.y, 0f);
        Vector3 vector3B = new Vector3(vectorB.x, vectorB.y, 0f);
        
        if (vector3A == vector3B)
            return 0f;
        if (vector3A.y == vector3B.y)
            return 0f;
        if (vector3A.x == vector3B.x)
            return 0f;

        // refVector is a 90cw rotation of vector3A
        Vector3 refVector = Vector3.Cross(vector3A, Vector3.forward);
        float dotProduct = Vector3.Dot(refVector, vector3B);

        if (dotProduct > 0)
            return Vector3.Angle(vector3A, vector3B);
        else if (dotProduct < 0)
            return -Vector3.Angle(vector3A, vector3B);
        else
            throw new System.InvalidOperationException("the vectors are opposite");
    }

}
