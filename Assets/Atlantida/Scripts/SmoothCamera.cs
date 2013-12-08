using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour {
	
    public Transform target;
	public Transform target_look;
    public float distance = 0f;
    public float height = 0.8399999f;
    public float damping = 6f;
    public bool smoothRotation = true;
    public bool followBehind = true;
    public float rotationDamping = 10f;
	
	void Update () {
	   Vector3 wantedPosition;
	   if(followBehind)
			wantedPosition = target.TransformPoint(0, height, -distance);
	   else
	        wantedPosition = target.TransformPoint(0, height, distance);
	
	   transform.position = Vector3.Lerp (transform.position, wantedPosition, Time.deltaTime * damping);
	
	   if (smoothRotation) {
	        Quaternion wantedRotation = Quaternion.LookRotation(target_look.position - transform.position);
	        transform.rotation = Quaternion.Slerp (transform.rotation, wantedRotation, Time.deltaTime * rotationDamping);
	   }
	   //else transform.LookAt (target_look, target_look.up);
	}
}