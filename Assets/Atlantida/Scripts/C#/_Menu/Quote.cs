using UnityEngine;
using System.Collections;

public class Quote : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke("NextScene", 4);
	}
	
	void NextScene()
	{
		AutoFade.LoadLevel(2,1,1,Color.black);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
