using UnityEngine;
using System.Collections;

public class KeepSound : MonoBehaviour {
    
	void Awake() {
        DontDestroyOnLoad(transform.gameObject);
    }
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Application.loadedLevel == 2){
			this.audio.volume = 0;
		}
	}
}
